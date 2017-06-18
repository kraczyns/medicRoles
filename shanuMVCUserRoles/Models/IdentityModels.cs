using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace shanuMVCUserRoles.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Specjalizacja")]
        public Specialization? Specialization { get; set; }

        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Zwalidowane")]
        public bool isValid { get; set; } = false;

        public virtual ICollection<Appointment> Appointments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum Place
    {
        Fiołkowa,
        Różana,
        Konwaliowa
    }

    public class Appointment
    {
        public int AppointmentID { get; set; }

        [Display(Name = "Lekarz")]
        public string DoctorID { get; set; }

        [Display(Name = "Pacjent")]
        public string PatientID { get; set; } = "free";

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Termin")]
        public DateTime Date { get; set; }

        [Display(Name = "Miejsce")]
        public Place Place { get; set; }

        public virtual ApplicationUser Doctor { get; set; }
        public virtual ApplicationUser Patient { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<shanuMVCUserRoles.Models.Appointment> Appointments { get; set; }
    }
}