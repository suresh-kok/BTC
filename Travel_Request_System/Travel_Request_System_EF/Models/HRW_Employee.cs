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
    using System.ComponentModel.DataAnnotations;

    public partial class HRW_Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRW_Employee()
        {
            this.ORG_EmpEntityLink = new HashSet<ORG_EmpEntityLink>();
            this.ORG_EntityManager = new HashSet<ORG_EntityManager>();
            this.Users = new HashSet<Users>();
        }
    
        public int TransactType { get; set; }
        public long TransactUserID { get; set; }
        public System.DateTime TransactDateTime { get; set; }
        public string MenuID { get; set; }
        [Key]
        public long EmployeeID { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Nullable<int> Salutation { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<System.DateTime> TerminationDate { get; set; }
        public string PrevEmpID { get; set; }
        public string Remarks { get; set; }
        public string RecordType { get; set; }
        public Nullable<long> PAFRecordID { get; set; }
        public string EmpStatus { get; set; }
    
        public virtual HRW_Company HRW_Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EmpEntityLink> ORG_EmpEntityLink { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORG_EntityManager> ORG_EntityManager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
