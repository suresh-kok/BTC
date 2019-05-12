using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class TravelAgencyMetadata
    {
        [DisplayName("Travel Agency ID")]
        [Required(ErrorMessage = "Travel Agency ID is Required")]
        public int ID { get; set; }

        [DisplayName("Agency Code")]
        [Required(ErrorMessage = "Agency Code is Required")]
        public string AgencyCode { get; set; }

        [DisplayName("Company")]
        [Required(ErrorMessage = "Company is Required")]
        public string CompanyName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Telephone")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number")]
        public string Telephone { get; set; }

        [DisplayName("Fax")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid fax number")]
        public string Fax { get; set; }

        [DisplayName("Mobile")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

        [DisplayName("Landline")]
        public string Landline { get; set; }

        [DisplayName("Contact Person")]
        [Required(ErrorMessage = "Contact Persion is Required")]
        public string ContactPerson { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Is Active")]
        public string IsActive { get; set; }
    }
}