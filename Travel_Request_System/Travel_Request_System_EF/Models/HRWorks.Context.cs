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
    
    public partial class BTCEntities : DbContext
    {
        public BTCEntities()
            : base("name=BTCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ATQuotation> ATQuotation { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<HSQuotation> HSQuotation { get; set; }
        public virtual DbSet<LPO> LPO { get; set; }
        public virtual DbSet<PCQuotation> PCQuotation { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TravelRequests> TravelRequests { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<RFQ> RFQ { get; set; }
        public virtual DbSet<TravelAgency> TravelAgency { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<AttachmentLink> AttachmentLink { get; set; }
        public virtual DbSet<Quotation> Quotation { get; set; }
    }
}
