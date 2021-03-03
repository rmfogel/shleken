using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Userservice
    {
        public static int AccountId { get { return 1; } }
        public static int UserId { get; set; }

        public static List<UsersViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Users

                    .Map<Users, UsersViewModel>();
            }
        }

        public static List<UsersViewModel> Get(int projectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var t = db.Projects.ToList().FirstOrDefault(i => i.Id == projectId).RefUserPro.Select(i => i.Users).ToList();
                var s= t.Map<Users, UsersViewModel>();
                return t.Map<Users, UsersViewModel>();
            }
        }

        public static List<RefUserProViewModel> Get(int projectId,UsersSearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                var users= db.Projects.ToList().FirstOrDefault(i => i.Id == projectId).RefUserPro.ToList();
                if (!string.IsNullOrEmpty(search.Name))
                    users = users.Where(n => n.Users.Name.Contains(search.Name)).ToList();
                if (!string.IsNullOrEmpty(search.Role))
                    users = users.Where(r => r.ProjecrId == projectId&& r.Roles.Name==search.Role).ToList();
                        var u= users.ToList().Map<RefUserPro, RefUserProViewModel>();
                for (int i = 0; i < users.Count; i++)
                {
                    u[i].ProjectId = users[i].ProjecrId;
                    u[i].ProjectName = users[i].Projects.Name;
                    u[i].Email = users[i].Users.Email;
                    u[i].Name = users[i].Users.Name;
                    u[i].IsOwner = db.Projects.ToList().FirstOrDefault(j => j.Id == projectId).RefUserPro.Min(o => o.Id) == users[i].Users.Id;
                    u[i].Password = users[i].Users.Password;
                    u[i].Task = db.TasksRoles.ToList().FirstOrDefault(g=>g.RoleId==users[i].RoleId);

                }
                return u;
            }
        }



        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Users.Single(i => i.Id == id);
                db.Users.Remove(item);
                db.SaveChanges();
            }
        }

        public static bool IsProjectManager(int projects, int userId)
        {
            ShlekenEntities3 db = new ShlekenEntities3();
            RefUserPro user = db.RefUserPro.ToList().FirstOrDefault(u => u.ProjecrId == projects && u.UserId == userId);

            bool isM = db.Projects.ToList().FirstOrDefault(j => j.Id == projects).RefUserPro.Min(o => o.Id) == user.Id;

            db.RefUserPro.ToList().FirstOrDefault(u => u.ProjecrId == projects && u.UserId == userId).IsOwner = isM;
            db.SaveChanges();
            return isM;


           
        }

        public static void Delete(int id,int projectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Users.Single(i => i.RefUserPro.Any(r=>r.Id==id));
                var tasks = db.RefUserPro.Where(t => t.UserId == item.Id).SelectMany(y => y.Roles.TasksRoles);
                db.TasksRoles.RemoveRange(tasks);
                db.RefUserPro.RemoveRange(db.RefUserPro.Where(r => r.ProjecrId == projectId && r.UserId == item.Id));
                db.Teams.RemoveRange(db.Teams.Where(r => r.ProjectId == projectId && r.UserId == item.Id));
                db.SaveChanges();
            }
        }

        public static object Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Users.
                     Single(i => i.Id == id)
                    .Map<Users, UsersViewModel>();
            }
        }

        public static object Single(int id,int projectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var x= db.RefUserPro.
                     Single(i => i.Id==id&&i.ProjecrId==projectId)
                    .Map<RefUserPro, RefUserProViewModel>();
                x.SetViewModel();
                return x;
            }
        }

        public static void Save(UsersViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Users.Single(i =>  i.Id == model.Id);
                    item = model.Map<UsersViewModel, Users>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<UsersViewModel, Users>();
                    item.AccountId = Userservice.AccountId;
                    db.Users.Add(item);
                    db.SaveChanges();
                }
            }


        }

        public static void Save(RefUserProViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Users.Single(i => i.RefUserPro.Any(u=>u.Id==model.Id));
                    int id = item.Id;
                    item = model.Map<UsersViewModel, Users>(item);
                    item.Id = id;
                    db.SaveChanges();
                }
                else
                {
                    
                    db.TasksRoles.Add(new TasksRoles { RoleId = model.Task.RoleId, TaskId = Taskservice.GetTemp(model.ProjectId)});
                    db.SaveChanges();
                    if (model.UserId == 0)
                    {
                        db.Users.Add(new Users { Email = model.Email, Password = model.Password, Name = model.Name, AccountId = db.Accounts.FirstOrDefault().Id });
                        db.SaveChanges();
                        model.UserId = db.Users.ToList().LastOrDefault().Id;
                    }
                    int roleId = db.TasksRoles.ToList().LastOrDefault().RoleId;
                    db.RefUserPro.Add(new RefUserPro { ProjecrId=model.ProjectId,RoleId= roleId, UserId=model.UserId});
                    var y = db.TasksRoles.ToList().LastOrDefault();
                    db.SaveChanges();
                    List<TasksRoles> tasks = new List<TasksRoles>();
                    foreach (var item in db.Projects.Single(p=>p.Id== model.ProjectId).Tasks)
                    {
                        if (db.TasksRoles.Any(t=>t.TaskId==item.Id&&t.RoleId==roleId)==false)
                            tasks.Add(new TasksRoles {TaskId=item.Id,RoleId= roleId,Value=0 });
             }
                    db.TasksRoles.AddRange(tasks);
                    db.SaveChanges();
                }
            }


        }


        public static List<ProjectsViewModel> GetProjectsForUsers(int id)
        {
            var t= new ShlekenEntities3().Projects.Where(p => p.RefUserPro.Any(i => i.UserId == id)).ToList().Map<Projects, ProjectsViewModel>();
            var s = new ShlekenEntities3().RefUserPro.ToList();
            //var s =  new ShlekenEntities3().Users.Where(i => i.Id == id).FirstOrDefault().RefUserPro.ToList().Select(p => p.Projects).Map<Projects, ProjectsViewModel>();
            return new ShlekenEntities3().Projects.Where(p => p.RefUserPro.Any(i => i.UserId == id)).ToList().Map<Projects, ProjectsViewModel>();
        }
       

        public static List<MeetingsViewModel> GetMeetingsForUser(int id)
        {
            var db = new ShlekenEntities3();
            var lm = new List<MeetingsViewModel>();
            lm = db.Projects.Where(p => p.RefUserPro.Any(i => i.UserId == id)).ToList().SelectMany(pr => pr.Meetings.Map<Meetings, MeetingsViewModel>()).ToList();
            return lm;
        }

        public static List<RefUserProViewModel> GetRefUsers(int projectId)
        {
            ShlekenEntities3 db = new ShlekenEntities3();
            var refs = db.RefUserPro.Where(u => u.ProjecrId == projectId).ToList();
            foreach (var item in refs)
            {
                if (item.Roles.TasksRoles != null)
                    continue;
                item.Roles = new Roles { };
            }
            foreach (var item in db.Tasks.Where(t=>t.ProjectId==projectId).ToList())
            {
               if(item.TasksRoles.Count==refs.Count)
                {

                }

            }

            return refs.Map<RefUserPro, RefUserProViewModel>();
        }
    }
}
