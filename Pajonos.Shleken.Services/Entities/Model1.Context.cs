﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pajonos.Shleken.Services.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShlekenEntities3 : DbContext
    {
        public ShlekenEntities3()
            : base("name=ShlekenEntities3")
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
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ProjectStatuses> ProjectStatuses { get; set; }
        public virtual DbSet<RefUserPro> RefUserPro { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<RiskPriorities> RiskPriorities { get; set; }
        public virtual DbSet<RiskProbabilities> RiskProbabilities { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<RiskStatuses> RiskStatuses { get; set; }
        public virtual DbSet<RoleResources> RoleResources { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksRoles> TasksRoles { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<ToDoLists> ToDoLists { get; set; }
        public virtual DbSet<ToDoListStatuses> ToDoListStatuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public System.Data.Entity.DbSet<Pajonos.Shleken.Services.Models.AccountViewModel> AccountViewModels { get; set; }
    }
}
