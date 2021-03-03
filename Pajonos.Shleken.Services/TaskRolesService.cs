using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class TaskRolesService
    {
        public static void Save(TasksRolesViewModel model)
        {
            using (var db = new ShlekenEntities())
            {
                var item = db.TasksRoles.SingleOrDefault(i => i.TaskId == model.TaskId && i.RoleId == model.RoleId);
                if (item != null)
                {
                    item = model.Map<TasksRolesViewModel, TasksRole>(item);
                    db.SaveChanges();
                }
             else
                {
                    var newItem = model.Map<TasksRolesViewModel, TasksRole>();
                    db.TasksRoles.Add(newItem);
                    db.SaveChanges();
                }


            }
        }

    }
}
