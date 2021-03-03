using Pajonos.Shleken.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public class RefUserProViewModel:UsersViewModel
    {
        public  List<TasksRoles> Tasks { get; set; }
        public TasksRoles Task { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }


        public string FullName
        {
            get { return Users.Name+" "+Roles.Name; }
        }


        public int Value
        {
            set
            {
                ShlekenEntities3 db = new ShlekenEntities3();
                db.TasksRoles.Where(t => t.RoleId == RoleId).ToList().ForEach(t => t.Value = value);
                db.SaveChanges();
            }

            get
            {
                ShlekenEntities3 db = new ShlekenEntities3();
                int a = 100;
                if (db.TasksRoles.Where(t => t.RoleId == RoleId).FirstOrDefault() != null)
                 a=db.TasksRoles.Where(t => t.RoleId == RoleId).FirstOrDefault().Value;
                if (a == 0)
                    a = 100;
                return a;
            }
      }





        public int UserId { get; set; }
        public int ProjecrId { get; set; }
        public bool IsOwner { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual Projects Projects { get; set; }
        public virtual Users Users { get; set; }
        public virtual Roles Roles { get; set; }

        public RefUserProViewModel():base()
        {
            
            ShlekenEntities3 db = new ShlekenEntities3();
            ProjectId = ProjecrId;
            Tasks = db.TasksRoles.Where(t => t.RoleId == RoleId).ToList();

        }

        public void SetViewModel()
        {
            ShlekenEntities3 db = new ShlekenEntities3();
            ProjectId = ProjecrId;

            Tasks = db.TasksRoles.Where(t => t.RoleId == RoleId).ToList();
            db.TasksRoles.Where(t => t.RoleId == RoleId).ToList().ForEach(t => t.Value = Value);
            var user = db.RefUserPro.FirstOrDefault(r => r.Id == Id).Users;
            Password = user.Password;
            Email = user.Email;
            Name = user.Name;
            Roles = db.Roles.FirstOrDefault(t => t.Id == RoleId);
            RoleName = Roles.Name;

            db.SaveChanges();

        }


    }
}
