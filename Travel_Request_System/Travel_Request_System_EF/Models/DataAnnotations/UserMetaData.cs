using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class UserMetaData
    {
        [DisplayName("User ID")]
        [Required(ErrorMessage = "User ID is Required")]
        public int ID { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is Required")]
        [MaxLength(100)]
        public string Username { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email ID is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Activation Code")]
        public System.Guid ActivationCode { get; set; }

        [DisplayName("BTC Employee Code")]
        [Required(ErrorMessage = "BTC Employee Code is Required")]
        public Nullable<long> HREmployeeID { get; set; }

        [DisplayName("Is Deleted")]
        public Nullable<bool> IsDeleted { get; set; }
    }
}