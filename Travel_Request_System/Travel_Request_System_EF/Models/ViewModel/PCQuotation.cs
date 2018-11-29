using System;
using System.Collections.Generic;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class PCQuotation
    {
        public int PCQuotationID { get; set; }
        public int QuotationID { get; set; }
        public string TravelSectorPC { get; set; }
        public string PickUpLocation { get; set; }
        public Nullable<DateTime> PickUpDate { get; set; }
        public Nullable<TimeSpan> PickUpTime { get; set; }
        public string DropOffLocation { get; set; }
        public Nullable<DateTime> DropOffDate { get; set; }
        public Nullable<TimeSpan> DropOffTime { get; set; }
        public string PreferredVehicle { get; set; }
        public Decimal PCQAmount { get; set; }
        public List<string> PCQAttachment { get; set; }
        public bool IsDeleted { get; set; }
    }
}