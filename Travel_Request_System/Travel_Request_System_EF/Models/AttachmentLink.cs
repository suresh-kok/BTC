//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Travel_Request_System_EF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttachmentLink
    {
        public int ID { get; set; }
        public string AttachmentFor { get; set; }
        public int AttachmentForID { get; set; }
    
        public virtual Attachments Attachments { get; set; }
    }
}
