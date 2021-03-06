//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Projects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Projects()
        {
            this.Activities = new HashSet<Activities>();
            this.Contacts = new HashSet<Contacts>();
            this.Files = new HashSet<Files>();
            this.Fixes = new HashSet<Fixes>();
            this.Globals = new HashSet<Globals>();
            this.Incomes = new HashSet<Incomes>();
            this.Meetings = new HashSet<Meetings>();
            this.Outcomes = new HashSet<Outcomes>();
            this.RefUserPro = new HashSet<RefUserPro>();
            this.Resources = new HashSet<Resources>();
            this.Risks = new HashSet<Risks>();
            this.RoleResources = new HashSet<RoleResources>();
            this.Roles = new HashSet<Roles>();
            this.Tasks = new HashSet<Tasks>();
            this.Teams = new HashSet<Teams>();
            this.ToDoLists = new HashSet<ToDoLists>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public double TaskTotalCost { get; set; }
        public double TaskTotalHours { get; set; }
        public double TeamTotalHours { get; set; }
        public double TeamTotalCost { get; set; }
        public string JiraUrl { get; set; }
        public string JiraPassword { get; set; }
        public string JiraUserName { get; set; }
        public string JiraProjectkey { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activities> Activities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacts> Contacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Files> Files { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fixes> Fixes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Globals> Globals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incomes> Incomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meetings> Meetings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Outcomes> Outcomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefUserPro> RefUserPro { get; set; }
        public virtual Projects Projects1 { get; set; }
        public virtual Projects Projects2 { get; set; }
        public virtual ProjectStatuses ProjectStatuses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resources> Resources { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Risks> Risks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleResources> RoleResources { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roles> Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teams> Teams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToDoLists> ToDoLists { get; set; }
    }
}
