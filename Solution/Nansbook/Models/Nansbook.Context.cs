﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nansbook.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NansbookEntities : DbContext
    {
        public NansbookEntities()
            : base("name=NansbookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<CommissionRate> CommissionRates { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Paycheck> Paychecks { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserXStore> UserXStores { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
    }
}
