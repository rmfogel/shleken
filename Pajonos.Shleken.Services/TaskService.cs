using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
    public static class Taskservice
    {
        public static List<TasksViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var Roles = RolesService.Get(ProjectId);
             var listTasks= db.Tasks
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                   .ToList()
                         .Select(i =>
                         {
                             var item = i.Map<Entities.Tasks, TasksViewModel>();
                             item.AllTasksRoles = new List<TasksRoles>();
                             foreach (var rols in Roles)
                             {
                                 var cnt = 1;
                                 foreach (var Hour in item.TasksRoles)
                                 {
                                     if (Hour.RoleId == rols.Id && Hour.TaskId == item.Id)
                                     {
                                         item.AllTasksRoles.Add(Hour);
                                     }
                                     else
                                     {
                                         if (item.TasksRoles.Count == cnt)
                                         {
                                             var RolesEmpty = new TasksRoles();
                                             RolesEmpty.Value = 0;
                                             RolesEmpty.RoleId = rols.Id;
                                             item.AllTasksRoles.Add(RolesEmpty);
                                         }
                                         cnt++;
                                     }
                                     
                                 }
                                 if (item.AllTasksRoles.Count() == 0)
                                 {
                                     var RolesEmpty = new TasksRoles();
                                     RolesEmpty.Value = 0;
                                     RolesEmpty.RoleId = rols.Id;
                                     item.AllTasksRoles.Add(RolesEmpty);
                                 }
                             }
                             return item;
                         }).ToList();
                return listTasks.Where(t=>t.Name!="no task").ToList();
            }
        }

        public static int GetTemp(int pro)
        {
            ShlekenEntities3 db = new ShlekenEntities3();
       var oo= db.Tasks.ToList().FirstOrDefault(t => t.Name == "no task" && t.ProjectId == pro);
            if (oo == null)
            {
                db.Tasks.Add(new Tasks { Name = "no task", ProjectId = pro, Description = "h", Status = "done", ShowClient = false });
                db.SaveChanges();
                GetTemp(pro);
            }
            return oo.Id;
          
        }

        public static void Save(List<TasksViewModel> models)
        {
            using (var db = new ShlekenEntities3())
            {
                foreach (var model in models)
                {
                    if (model.Id > 0)
                    {
                        var tr = model.TasksRoles;
                        model.TasksRoles = null;
                        var item = db.Tasks.SingleOrDefault(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                        item = model.Map<TasksViewModel, Entities.Tasks>(item);
                        db.SaveChanges();
                        if (tr != null)
                        {
                            SaveRolestaks(tr, item.Id);
                        }
                     
                    }
                    else
                    {
                        if (model.Name != null)
                        {
                            var item = model.Map<TasksViewModel, Entities.Tasks>();
                            db.Tasks.Add(item);
                            db.SaveChanges();
                            var TasksAfterSave = db.Tasks.ToList().Last();
                            if (model.TasksRoles != null)
                            {
                                SaveRolestaks(model.TasksRoles, TasksAfterSave.Id);
                            }
                        }
                       
                       
                    }
                }
            }
        }

        public static void SaveRolestaks(ICollection<TasksRoles> TasksRoles, int TaskId)
        {
            using (var db = new ShlekenEntities3())
            {
                foreach (var model in TasksRoles)
                {
                    model.TaskId = TaskId;
                    var item = db.TasksRoles.SingleOrDefault(i => i.RoleId == model.RoleId&&i.TaskId==TaskId);
                    if (item != null)
                    {
                        item.TaskId = model.TaskId;
                        item.RoleId = model.RoleId;
                        item.Value = model.Value;
                        db.SaveChanges();
                    }
                    else
                    {
                        TasksRoles newItem = new TasksRoles
                        {
                            TaskId = model.TaskId,
                            RoleId = model.RoleId,
                            Value = model.Value
                        };
                        db.TasksRoles.Add(newItem);
                        db.SaveChanges();
                    }
                }
            }
        }


        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Tasks.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);

                var TasksRoles = db.TasksRoles.Where(i => i.TaskId == item.Id && i.Tasks.ProjectId == item.ProjectId && i.Tasks.Projects.AccountId == item.Projects.AccountId).ToList();
                foreach (var taskrols in TasksRoles)
                {
                    db.TasksRoles.Remove(taskrols);
                }

                db.Tasks.Remove(item);
                db.SaveChanges();

            }
        }

        public static int GetNumOfProjectStatuses(string status)
        {

            return  new ShlekenEntities3().Tasks.ToList().Where(s =>s.Status!=null&& s.Status.Trim().ToLower() == status.Trim().ToLower()).Count();
        }

        public static int GetNumOfProjectStatuses(string status,int project)
        {

            return new ShlekenEntities3().Tasks.ToList().Where(s =>s.ProjectId==project&& s.Status != null && s.Status.Trim().ToLower() == status.Trim().ToLower()).Count();
        }
    }
}
