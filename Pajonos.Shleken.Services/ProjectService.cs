using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Projectservice
    {

        public static List<ProjectsViewModel> Get(ProjectsSearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Projects
                    .Where(i => i.AccountId == Userservice.AccountId &&
                    (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower())) &&
                    //(search.OwnerId == 0 || i.OwnerId == search.OwnerId) &&
                    (search.Status == 0 || i.Status == search.Status))
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Projects, ProjectsViewModel>();
                        if (i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault() == null&& i.RefUserPro.ToList().FirstOrDefault() != null)
                                i.RefUserPro.ToList().FirstOrDefault().IsOwner = true;
                        if (i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault() != null)
                        item.OwnerName = i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault().Users.Name;
                        item.StatusName = i.ProjectStatuses.Name;
                        return item;
                    })
                    .ToList();
            }
        }

        public static List<ProjectsViewModel> Get(ProjectsSearchViewModel search,int userId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Projects.ToList().Where(p=>p.RefUserPro.Any(r=>r.UserId==userId))
                    .Where(i => i.AccountId == Userservice.AccountId &&
                    (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower())) &&
                    //(search.OwnerId == 0 || i.OwnerId == search.OwnerId) &&
                    (search.Status == 0 || i.Status == search.Status))
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Projects, ProjectsViewModel>();
                        if (i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault() == null && i.RefUserPro.ToList().FirstOrDefault() != null)
                            i.RefUserPro.ToList().FirstOrDefault().IsOwner = true;
                        if (i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault() != null)
                            item.OwnerName = i.RefUserPro.Where(o => o.IsOwner == true).FirstOrDefault().Users.Name;
                        item.StatusName = i.ProjectStatuses.Name;
                        return item;
                    })
                    .ToList();
            }
        }


        public static ProjectsViewModel Get(int Projects)
        {
            using (var db = new ShlekenEntities3())
            {
                var getProjects = db.Projects
                    .SingleOrDefault(i => i.AccountId == Userservice.AccountId && i.Id == Projects);
                      var item = getProjects.Map<Projects, ProjectsViewModel>();
                return item;
            }
        }



        public static List<ProjectsViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Projects
                    .Where(i => i.AccountId == Userservice.AccountId && i.Status == 1)
                    .Map<Projects, ProjectsViewModel>();
            }
        }


        public static ProjectsViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Projects
                        .Single(i => i.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Projects, ProjectsViewModel>();
            }
        }

        public static int GetLast()
        {
            return new ShlekenEntities3().Projects.ToList().Last().Id;
        }

        public static void Save(ProjectsViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<ProjectsViewModel, Projects>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<ProjectsViewModel, Projects>();
                    db.Projects.Add(item);
                    db.SaveChanges();
                    db.Tasks.Add(new Tasks { Name="no task",ProjectId=db.Projects.ToList().LastOrDefault().Id,Description="h",Status="done",ShowClient=false});
                    db.SaveChanges();
                }
            }
        }

        public static void SaveTasksHoursCost(int TasksHours, int TasksCost, int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectId);
                item.TaskTotalCost = TasksCost;
                item.TaskTotalHours = TasksHours;
                db.SaveChanges();
            }
        }

        public static void SaveTeamsHoursCost(int TeamsHours, int TeamsCost, int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectId);
                item.TeamTotalCost = TeamsCost;
                item.TeamTotalHours = TeamsHours;
                db.SaveChanges();
            }
        }

        public static void GetHoursCost(int ProjectId,ref bool hours, ref bool cost)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectId);
                if (item.TeamTotalHours > item.TaskTotalHours)
                {
                    hours = true;
                }
                if (item.TeamTotalCost > item.TaskTotalCost)
                {
                    cost = true;
                }
            }
        }



        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == id);
                var list = db.Contacts.ToList();
                foreach (var i in list)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Contacts.Remove(i);
                        db.SaveChanges();
                    }
                }

                var m = db.Meetings.ToList();
                foreach (var i in m)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Meetings.Remove(i);
                        db.SaveChanges();
                    }
                }

                var tr = db.TasksRoles.ToList();
                foreach (var i in tr)
                {
                    if (i.Tasks.ProjectId == item.Id && i.Roles.ProjectId == item.Id && i.Tasks.Projects.AccountId == item.AccountId && i.Roles.Projects.AccountId == item.AccountId)
                    {
                        db.TasksRoles.Remove(i);
                        db.SaveChanges();
                    }
                }


                var r = db.Tasks.ToList();
                foreach (var i in r)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Tasks.Remove(i);
                        db.SaveChanges();
                    }
                }

                var g = db.Globals.ToList();
                foreach (var i in g)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Globals.Remove(i);
                        db.SaveChanges();
                    }
                }

                var Teams = db.Teams.ToList();
                foreach (var i in Teams)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Teams.Remove(i);
                        db.SaveChanges();
                    }
                }

                var res = db.Roles.ToList();
                foreach (var i in res)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Roles.Remove(i);
                        db.SaveChanges();
                    }
                }

                var Fixes = db.Fixes.ToList();
                foreach (var i in Fixes)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Fixes.Remove(i);
                        db.SaveChanges();
                    }
                }


                var a = db.Activities.ToList();
                foreach (var i in a)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Activities.Remove(i);
                        db.SaveChanges();
                    }
                }


                var o = db.Outcomes.ToList();
                foreach (var i in o)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Outcomes.Remove(i);
                        db.SaveChanges();
                    }
                }


                var n = db.Incomes.ToList();
                foreach (var i in n)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Incomes.Remove(i);
                        db.SaveChanges();
                    }
                }

                var f = db.Files.ToList();
                foreach (var i in f)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Files.Remove(i);
                        db.SaveChanges();
                    }
                }

                var Risks = db.Risks.ToList();
                foreach (var i in Risks)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.Risks.Remove(i);
                        db.SaveChanges();
                    }
                }

                var t = db.ToDoLists.ToList();
                foreach (var i in t)
                {
                    if (i.ProjectId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.ToDoLists.Remove(i);
                        db.SaveChanges();
                    }
                }

                var re = db.RefUserPro.ToList();
                foreach (var i in re)
                {
                    if (i.ProjecrId == item.Id && i.Projects.AccountId == item.AccountId)
                    {
                        db.RefUserPro.Remove(i);
                        db.SaveChanges();
                    }
                }
                db.Projects.Remove(item);
                db.SaveChanges();
            }
        }

        public static ProjectsViewModel GetnameProjects(int Projects)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Projects
                        .Single(i => i.AccountId == Userservice.AccountId && i.Id == Projects)
                        .Map<Projects, ProjectsViewModel>();
            }

        }

       
    }


}
