using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
    public static class RolesService
    {
        public static List<RolesResourcesViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Roles
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Map<Roles, RolesResourcesViewModel>();
            }
        }


        public static int countRoles(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Roles
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Count();
            }
        }

        public static int GetLast()
        {
            return new ShlekenEntities3().Roles.ToList().LastOrDefault().Id;
        }


        public static int Add(RolesResourcesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = model.Map<RolesResourcesViewModel, Roles>();
                db.Roles.Add(item);
                db.SaveChanges();
                return item.Id;
            }
        }

        public static void New(int pro,string name)
        {
            using (var db = new ShlekenEntities3())
            {
                db.Roles.Add(new Roles { Name=name,ProjectId=pro});
                db.SaveChanges();
            }
         }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var user = db.RefUserPro.Where(u => u.Id == id).FirstOrDefault();
                var item = db.Roles.Single(i =>  i.Id == user.RoleId);
                var list = db.Teams.ToList();
                foreach (var i in list)
                {
                    if (i.ProjectId == item.ProjectId  && i.RoleId == item.Id)
                    {
                        db.Teams.Remove(i);
                        db.SaveChanges();
                    }
                }

                var TasksRoles = db.TasksRoles.ToList();
                foreach (var t in TasksRoles)
                {
                    if (t.Roles.ProjectId == item.ProjectId && t.RoleId == item.Id)
                    {
                        db.TasksRoles.Remove(t);
                        db.SaveChanges();
                    }
                }
                db.Roles.Remove(item);
                db.RefUserPro.Remove(user);
                db.SaveChanges();
            }
        }

        public static void AddRoleForUser(RolesResourcesViewModel model,int user)
        {
            TasksRoles task = new TasksRoles
            {
                RoleId = RolesService.Add(model)
            };

            Userservice.Save(new RefUserProViewModel {UserId=user,Task=task,Tasks = new List<TasksRoles>{task},ProjecrId=model.ProjectId,ProjectId=model.ProjectId
            });
        }

        
    }
}
