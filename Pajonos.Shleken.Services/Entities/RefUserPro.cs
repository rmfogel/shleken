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
    
    public partial class RefUserPro
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjecrId { get; set; }
        public bool IsOwner { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual Users Users { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
