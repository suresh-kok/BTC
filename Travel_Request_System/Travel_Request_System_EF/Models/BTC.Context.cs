﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HRWorksEntities : DbContext
    {
        public HRWorksEntities()
            : base("name=HRWorksEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BTCEmployeeInfo> BTCEmployeeInfoes { get; set; }
        public virtual DbSet<BTCLoginInfo> BTCLoginInfoes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<HRW_Company> HRW_Company { get; set; }
        public virtual DbSet<HRW_EmpEntityParamValues> HRW_EmpEntityParamValues { get; set; }
        public virtual DbSet<HRW_Employee> HRW_Employee { get; set; }
        public virtual DbSet<ORG_ChartMaster> ORG_ChartMaster { get; set; }
        public virtual DbSet<ORG_EmpEntityLink> ORG_EmpEntityLink { get; set; }
        public virtual DbSet<ORG_EntityMaster> ORG_EntityMaster { get; set; }
        public virtual DbSet<ORG_EntityType> ORG_EntityType { get; set; }
        public virtual DbSet<ORG_EntityTypeParam> ORG_EntityTypeParam { get; set; }
        public virtual DbSet<TravelRequest> TravelRequests { get; set; }
        public virtual DbSet<TravelRequestDetail> TravelRequestDetails { get; set; }
    }
}
