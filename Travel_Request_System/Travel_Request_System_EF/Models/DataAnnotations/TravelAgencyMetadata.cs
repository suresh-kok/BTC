using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class TravelAgencyMetadata
    {
        public int ID { get; set; }

        [DisplayName("Agency Code")]
        [Required]
        public string AgencyCode { get; set; }

        [DisplayName("Company")]
        [Required]
        public string CompanyName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Telephone")]
        [Required]
        public string Telephone { get; set; }

        [DisplayName("Fax")]
        public string Fax { get; set; }

        [DisplayName("Mobile")]
        [Required]
        public string Mobile { get; set; }

        [DisplayName("Landline")]
        public string Landline { get; set; }

        [DisplayName("Contact Person")]
        [Required]
        public string ContactPerson { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Is Active")]
        public string IsActive { get; set; }
    }
}