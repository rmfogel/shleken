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
    
    public partial class ToDoLists
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public int IsDone { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int Userid { get; set; }
        public int ProjectId { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual ToDoListStatuses ToDoListStatuses { get; set; }
        public virtual Users Users { get; set; }
    }
}
