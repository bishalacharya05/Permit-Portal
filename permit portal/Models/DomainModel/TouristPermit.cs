using System.ComponentModel.DataAnnotations;

namespace permit_portal.Models.DomainModel
{
    public class TouristPermit
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Selected Location")]
        public string PermitLocation { get; set; }

        [Required]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        [Required]
        [Display(Name = "No. of People")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "Purpose of Visit")]
        public string Purpose { get; set; }
    }
}
