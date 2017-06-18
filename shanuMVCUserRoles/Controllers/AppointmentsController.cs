using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;

namespace shanuMVCUserRoles.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointment
        [Authorize(Roles = "Pacjent")]
        public ActionResult Index(string userId, Specialization? specializationId, DateTime? fromDate, DateTime? toDate, Place? place)
        {
            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            appointments = appointments.Where(a => a.PatientID == "free");
            if (specializationId != null)
            {
                appointments = appointments.Where(s => s.Doctor.Specialization == specializationId);
            }
            if (fromDate != null && toDate != null)
            {
                appointments = appointments.Where(s => s.Date >= fromDate).Where(s => s.Date <= toDate);
            }
            if (place != null)
            {
                appointments = appointments.Where(p => p.Place == place);
            }
            ViewBag.Id = userId;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.Place = place;
            ViewBag.Specialization = specializationId;

            var model = new AppointmentViewModel
            {
                Appointments = appointments.ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Lekarz")]
        // GET: Appointment/Create
        public ActionResult Create(string userId)
        {
            ApplicationUser doctor = db.Users.Find(userId);
            ViewBag.Name = doctor.Name + " " + doctor.Surname;
            ViewBag.Id = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,DoctorID,PatientID,Date,Place")] Appointment appointment)
        {
            var userId = User.Identity.GetUserId();
        //    var newAppointment = new Appointment { Date = appointment.Date, Place = appointment.Place, DoctorID = userId };
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }

            return View(appointment);
        }

        [Authorize(Roles = "Pacjent")]
        public ActionResult Summary(string patientid, int appointmentid)
        {
            Appointment appointment = db.Appointments.Find(appointmentid);
            appointment.PatientID = patientid;
            db.SaveChanges();
            ViewBag.PatientID = patientid;
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            return View(appointment);
        }

        [Authorize(Roles = "Pacjent")]
        public ActionResult PostEdit(int appointmentid)
        {
            Appointment appointment = db.Appointments.Find(appointmentid);
            appointment.PatientID = "free";
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,DoctorID,PatientID,Date,Place")] Appointment appointment)
        {
            //appointment.PatientID = "free";
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            ApplicationUser user = db.Users.Find(appointment.PatientID);
            var body = "<p>Email od: {0} ({1})</p><p>Wiadomość:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));  // replace with valid value 
            message.From = new MailAddress("karolinaaraczynska@gmail.com");  // replace with valid value
            message.Subject = "Odwołanie wizyty";
            message.Body = string.Format(body, "WebMedic", "karolinaaraczynska@gmail.com", "Wizyta, która miała się odbyć: " + appointment.Date + " została odwołana przez lekarza. Przepraszamy za problem.");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "karolinaaraczynska@gmail.com",  // replace with valid value
                    Password = "stokrotka1"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                try
                {
                    smtp.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
