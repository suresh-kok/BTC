using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class RFQMetaData
    {

        [DisplayName("RFQ ID")]
        [Required(ErrorMessage = "RFQ ID is Required")]
        public int ID { get; set; }

        [DisplayName("Travel Agency ID")]
        //[Required(ErrorMessage = "Travel Agency ID is Required")]
        public Nullable<int> TravelAgencyID { get; set; }

        [DisplayName("Travel Request ID")]
        [Required(ErrorMessage = "Travel Request ID is Required")]
        public Nullable<int> TravelRequestID { get; set; }

        [DisplayName("Created By User")]
        [Required(ErrorMessage = "Created By User is Required")]
        public Nullable<int> UserID { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Processing Status")]
        [Required(ErrorMessage = "Processing Status is Required")]
        public int Processing { get; set; }

        [DisplayName("Processing Section")]
        [Required(ErrorMessage = "Processing Section is Required")]
        public int ProcessingSection { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [DisplayName("RFQ Name")]
        public string RFQName { get; set; }
    }
}