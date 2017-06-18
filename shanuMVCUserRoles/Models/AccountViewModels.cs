using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace shanuMVCUserRoles.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
		[Required]
		[Display(Name = "Nazwa użytkownika")]

		public string UserName { get; set; }

		[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public enum Specialization
    {
        Internista,
        Kardiolog,
        Dermatolog,
        Ginekolog,
        Urolog,
        Laryngolog,
        Endokrynolog,
        Neurolog,
        Okulista
    }

    public enum UserRoles
    {
        Lekarz,
        Pacjent
    }

    public class AppointmentViewModel
    {
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

        public ApplicationUser Doctor;
        public ApplicationUser Patient;
        public ICollection<Appointment> Appointments;
    }

    public class EditViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Specjalizacja")]
        public Specialization? Specialization { get; set; }

        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

    }

    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Display(Name = "Specjalizacja")]
        public Specialization? Specialization { get; set; }

        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public ICollection<Appointment> Appointments;
    }

    public class RegisterViewModel
    {
		[Required]
		[Display(Name = "Rola")]
		public UserRoles UserRoles { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Nazwa użytkownika")]
		public string UserName { get; set; }

        [Display(Name = "Specjalizacja")]
        public Specialization? Specialization { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Niepoprawny PESEL.", MinimumLength = 11)]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name="Zatwierdzony")]
        public bool isValid { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
