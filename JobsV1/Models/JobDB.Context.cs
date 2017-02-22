﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobsV1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JobDBContainer : DbContext
    {
        public JobDBContainer()
            : base("name=JobDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<JobMain> JobMains { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<JobServices> JobServices { get; set; }
        public virtual DbSet<JobItinerary> JobItineraries { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<JobPickup> JobPickups { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<SupplierType> SupplierTypes { get; set; }
        public virtual DbSet<SupplierItem> SupplierItems { get; set; }
        public virtual DbSet<JobServicePickup> JobServicePickups { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<JobThru> JobThrus { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<JobPayment> JobPayments { get; set; }
        public virtual DbSet<CarCategory> CarCategories { get; set; }
        public virtual DbSet<CarUnit> CarUnits { get; set; }
        public virtual DbSet<CarDestination> CarDestinations { get; set; }
        public virtual DbSet<CarRate> CarRates { get; set; }
        public virtual DbSet<CarReservation> CarReservations { get; set; }
        public virtual DbSet<CarImage> CarImages { get; set; }
        public virtual DbSet<JobContact> JobContacts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<ProductImage> ProductImages1 { get; set; }
        public virtual DbSet<ProductCondition> ProductConditions { get; set; }
    }
}
