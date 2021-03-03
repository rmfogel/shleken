using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Pajonos.Shleken.Services.Entities;

namespace Pajonos.Shleken.Services.Models
{
   public class TeamsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string RolesName { get; set; }

        [Display(Name= "Name")]
        [Required]
        public int UserId { get; set; }

        public string Name { get; set; }

        [Required]
        public int ProjectId { get; set; }


        public double Price { get; set; }

        public System.DateTime Date { get; set; }

        public int Hours { get; set; }

        public List<HourDate> UsersHours { get; set; }

        public List<HourDate> AllHours { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Users Users { get; set; }

        

        public int Value
        {
            get {
                ShlekenEntities3 db = new ShlekenEntities3();
                int a = 100;
                if (db.TasksRoles.Where(t => t.RoleId == RoleId).FirstOrDefault() != null)
                    a = db.TasksRoles.Where(t => t.RoleId == RoleId).FirstOrDefault().Value;
                return a;
            }
            
        }


    }
    public class HourDate
    {
        public int Id;
        public int Hour;
        public DateTime Date;
        public string Usersname;
    }
    
}
