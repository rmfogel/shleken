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
    
    public partial class Risks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mitigations { get; set; }
        public int Priority { get; set; }
        public int Probabilty { get; set; }
        public bool Budget { get; set; }
        public bool Delivery { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public double Cost { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual RiskPriorities RiskPriorities { get; set; }
        public virtual RiskProbabilities RiskProbabilities { get; set; }
        public virtual Users Users { get; set; }
        public virtual RiskStatuses RiskStatuses { get; set; }
    }
}
