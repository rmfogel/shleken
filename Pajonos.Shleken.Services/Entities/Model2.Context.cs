﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this Files may cause unexpected behavior in your application.
//     Manual changes to this Files will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pajonos.Shleken.Services.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShlekenEntities : DbContext
    {
        public ShlekenEntities()
            : base("name=ShlekenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Fixes> Fixes { get; set; }
        public virtual DbSet<Globals> Globals { get; set; }
        public virtual DbSet<Incomes> Incomes { get; set; }
        public virtual DbSet<Meetings> Meetings { get; set; }
        public virtual DbSet<Outcomes> Outcomes { get; set; }
        public virtual DbSet<projects> projects { get; set; }
        public virtual DbSet<projectstatuses> projectstatuses { get; set; }
        public virtual DbSet<RefUsersPro> RefUsersPro { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<RisksPriorities> RisksPriorities { get; set; }
        public virtual DbSet<RisksProbabilities> RisksProbabilities { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<Riskstatuses> Riskstatuses { get; set; }
        public virtual DbSet<RolesResources> RolesResources { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksRoles> TasksRoles { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<ToDoLists> ToDoLists { get; set; }
        public virtual DbSet<ToDoListstatuses> ToDoListstatuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
