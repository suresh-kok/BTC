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
    
    public partial class ORG_EntityMasterParamValues
    {
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        public long EntityMasterParamID { get; set; }
        public long EntityId { get; set; }
        public long EntityTypeParamID { get; set; }
        public string ParamValue { get; set; }
    
        public virtual ORG_EntityMaster ORG_EntityMaster { get; set; }
        public virtual ORG_EntityTypeParam ORG_EntityTypeParam { get; set; }
    }
}
