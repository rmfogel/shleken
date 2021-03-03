using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
    public static class Teamservice
    {
        //public static List<TeamsViewModel> Get(int ProjectId)
        //{
        //    using (var db = new ShlekenEntities3())
        //    {
        //        return db.Teams
        //            .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
        //           .OrderBy(i => i.RoleId)
        //            .ToList()
        //            .Select(i =>
        //            {
        //                var item = i.Map<Teams, TeamsViewModel>();
        //                item.RolesName = i.Roles.Name;
        //                item.Name = i.Users.Name;
        //                return item;
        //            }).ToList();
        //    }
        //}


        public static List<TeamsViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var x = db.Teams.Select(i => new { i.UserId, i.Hours }).ToList();
                return db.Teams
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId
                    && i.Date > i.Projects.StartDate
                    && i.Date < i.Projects.EndDate
                    )
                   .OrderBy(i => i.RoleId)
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Teams, TeamsViewModel>();
                        item.RolesName = i.Roles.Name;
                        item.Name = i.Users.Name;
                        return item;
                    }).ToList();
            }
        }

        public static List<TeamsViewModel> GetHoursUsers(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var newTeams = new List<Teams>();
                db.Projects.Where(p => p.Id == ProjectId).SelectMany(p => p.RefUserPro).ToList().Where(p=>!db.Teams.Any(pp=>pp.RoleId==p.RoleId&&pp.UserId==p.UserId)).ToList().ForEach(i => newTeams.Add(new Teams { ProjectId = i.ProjecrId, RoleId = i.RoleId.Value, UserId = i.UserId,Date=DateTime.Today,Hours=0 ,Roles=i.Roles,Users=i.Users}));
                db.Teams.AddRange(newTeams);
                db.SaveChanges();
                var UsersRoles = new List<TeamsViewModel>();
                var datesProjects = DatesProjects(ProjectId);
                var ListTeams = new List<TeamsViewModel>();

                var listTeams = db.Teams
                          .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                         .OrderBy(i => i.RoleId)
                         .ToList()
                         .Select(i =>
                         {
                             var item = i.Map<Teams, TeamsViewModel>();
                             item.RolesName = i.Roles.Name;
                             item.Name = i.Users.Name;
                             return item;
                         }).ToList()
                            .GroupBy(i => new { i.UserId, i.RoleId })
                           .ToList();
                foreach (var Teams in listTeams)
                {
                    var item = new TeamsViewModel();
                    item.UsersHours = new List<HourDate>();
                    foreach (var t in Teams)
                    {
                        item.Id = t.Id;
                        item.Date = t.Date;
                        item.UserId = t.UserId;
                        item.Name = t.Name;
                        item.RolesName = t.RolesName;
                        item.RoleId = t.RoleId;
                        item.UsersHours.Add(new HourDate { Hour = t.Hours, Date = t.Date, Id = t.Id });
                    }
                    item.AllHours = new List<HourDate>();
                    foreach (var date in datesProjects)
                    {
                        var count = 1;
                        foreach (var hour in item.UsersHours)
                        {
                            if (hour.Date.Month == date.Date.Month && hour.Date.Year == date.Date.Year)
                            {
                                item.AllHours.Add(new HourDate { Hour = hour.Hour, Date = hour.Date, Id = hour.Id });
                            }
                            else
                            {
                                if (count == item.UsersHours.Count)
                                {
                                    item.AllHours.Add(new HourDate { Hour = 0, Date = date.Date });
                                }
                                count++;
                            }
                        }
                    }
                    ListTeams.Add(item);
                }
                var tt = new List<Teams>();

                return ListTeams.OrderBy(i => i.RoleId).ToList();
            }

        }

        public static int GetHoursForMonth(DateTime date1,int project=-1)
        {
            using (var db = new ShlekenEntities3())
            {
                var from =new DateTime(DateTime.Today.Year,date1.Month,1);
                var to = new DateTime(DateTime.Today.Year, date1.Month,DateTime.DaysInMonth(DateTime.Today.Year, date1.Month));
                var dates = DatesBySearch(from, to);
                var Teams = db.Teams
                   .ToList()
                    .GroupBy(i => new { i.Users })
                    .ToList();
                if(project!=-1)
                {
                    Teams = db.Teams.Where(p=>p.ProjectId==project)
                  .ToList()
                   .GroupBy(i => new { i.Users })
                   .ToList();

                }
                var listTeams = new List<TeamsViewModel>();
                foreach (var team in Teams)
                {
                    var item = new TeamsViewModel();
                    item.Name = team.Key.Users.Name;
                    item.UsersHours = new List<HourDate>();
                    var TeamsDate = team.GroupBy(i => i.Date);
                    foreach (var td in TeamsDate)
                    {
                        var sum = 0;
                        foreach (var t in td)
                        {
                            sum += t.Hours;
                        }
                        var UsersHours = new HourDate { Date = td.Key.Date, Hour = sum, Usersname = item.Name };
                        item.UsersHours.Add(UsersHours);
                    }

                    item.AllHours = new List<HourDate>();
                    foreach (var date in dates)
                    {
                        var count = 1;
                        foreach (var hour in item.UsersHours)
                        {
                            if (hour.Date.Month == date.Date.Month && hour.Date.Year == date.Date.Year)
                            {
                                item.AllHours.Add(new HourDate { Hour = hour.Hour, Date = hour.Date, Id = hour.Id, Usersname = hour.Usersname });
                            }
                            else
                            {
                                if (count == item.UsersHours.Count)
                                {
                                    item.AllHours.Add(new HourDate { Hour = 0, Date = date.Date, Id = 0, Usersname = hour.Usersname });
                                }
                                count++;
                            }
                        }
                    }
                    listTeams.Add(item);
                }
                return listTeams.Sum(p=>p.UsersHours.Where(u=>u.Date.Month==date1.Month&&u.Date.Year==date1.Year).Sum(u=>u.Hour));
            }

        }

        public static List<TeamsViewModel> GetHoursUsers(TeamsearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                var from = search.FromDate;
                var to = search.ToDate;
                var dates = DatesBySearch(from, to);
                var Teams = db.Teams
                   .Where(i => i.Projects.AccountId == Userservice.AccountId)
                   .ToList()
                    .GroupBy(i => new { i.Users })
                    .ToList();
                var listTeams = new List<TeamsViewModel>();
                foreach (var team in Teams)
                {
                    var item = new TeamsViewModel();
                    item.Name = team.Key.Users.Name;
                    item.UsersHours = new List<HourDate>();
                    var TeamsDate = team.GroupBy(i => i.Date);
                    foreach (var td in TeamsDate)
                    {
                        var sum = 0;
                        foreach (var t in td)
                        {
                            sum += t.Hours;
                        }
                        var UsersHours = new HourDate { Date = td.Key.Date, Hour = sum, Usersname = item.Name };
                        item.UsersHours.Add(UsersHours);
                    }

                    item.AllHours = new List<HourDate>();
                    foreach (var date in dates)
                    {
                        var count = 1;
                        foreach (var hour in item.UsersHours)
                        {
                            if (hour.Date.Month == date.Date.Month && hour.Date.Year == date.Date.Year)
                            {
                                item.AllHours.Add(new HourDate { Hour = hour.Hour, Date = hour.Date, Id = hour.Id , Usersname = hour.Usersname });
                            }
                            else
                            {
                                if (count == item.UsersHours.Count)
                                {
                                    item.AllHours.Add(new HourDate { Hour = 0, Date = date.Date, Id = 0, Usersname = hour.Usersname });
                                }
                                count++;
                            }
                        }
                    }
                    listTeams.Add(item);
                }
                return listTeams.Where(i => (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower()))).ToList();
            }
        }

        public static List<TeamsViewModel> GetAllMore180(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var datesProjects = DatesProjects(ProjectId);
                var Teams = db.Teams
                   .Where(i => i.Projects.AccountId == Userservice.AccountId)
                   .ToList()
                    .GroupBy(i => new { i.Users })
                    .ToList();
                var listTeams = new List<TeamsViewModel>();
                foreach (var team in Teams)
                {
                    var item = new TeamsViewModel();
                    item.Name = team.Key.Users.Name;
                    item.UserId = team.Key.Users.Id;
                    item.UsersHours = new List<HourDate>();
                    var TeamsDate = team.GroupBy(i => i.Date);
                    foreach (var td in TeamsDate)
                    {
                        var sum = 0;
                        foreach (var t in td)
                        {
                            sum += t.Hours;
                        }
                        var UsersHours = new HourDate { Date = td.Key.Date, Hour = sum ,Usersname=item.Name};
                        item.UsersHours.Add(UsersHours);
                    }

                    item.AllHours = new List<HourDate>();
                    foreach (var date in datesProjects)
                    {
                        var count = 1;
                        foreach (var hour in item.UsersHours)
                        {
                            if (hour.Date.Month == date.Date.Month && hour.Date.Year == date.Date.Year)
                            {
                                item.AllHours.Add(new HourDate { Hour = hour.Hour, Date = hour.Date, Id = hour.Id ,Usersname=hour.Usersname});
                            }
                            else
                            {
                                if (count == item.UsersHours.Count)
                                {
                                    item.AllHours.Add(new HourDate { Hour = 0, Date = date.Date, Id = 0 , Usersname = hour.Usersname });
                                }
                                count++;
                            }
                        }
                    }
                    listTeams.Add(item);
                }
                return listTeams;
            }
        }


        public static List<DateTime> DatesProjects(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                var month = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectId);
                var dates = new List<DateTime>();
                for (var i = new DateTime(month.StartDate.Year, month.StartDate.Month, 1); i < month.EndDate; i = i.AddMonths(1))
                {
                    dates.Add(i);
                }
                return dates;
            }
        }

        public static IEnumerable<TeamsViewModel> GetHoursMore180(List<TeamsViewModel> items, int Projects)
        {
            var allItems = items.Select(i =>
            {
                var item = new
                {
                    i.UserId
                };
                return item;
            }).ToList().Distinct();
            var allTeams = GetAllMore180(Projects);
            var JoinMore180 = allTeams.Join(allItems, t => t.UserId, ta => ta.UserId, (t, ta) => new { t, ta })
                .Select(i =>
                {
                    var item = new TeamsViewModel
                    {
                        Hours = i.t.Hours,
                        Name = i.t.Name,
                        Date = i.t.Date,
                        AllHours=i.t.AllHours
                    };
                    return item;
                }).ToList();
            var listAlert = new List<TeamsViewModel>();
          JoinMore180.Select(i => i.AllHours
             .Where(y => y.Hour > 180)
             .Select(y=> {
                 var item = new TeamsViewModel
                 {
                     Hours = y.Hour,
                     Date = y.Date,
                     Name=y.Usersname
                 };
                 listAlert.Add(item);
                 return item;
             }).ToList())
                 .ToList();
            return listAlert;
        }

        public static IEnumerable<TeamsViewModel> GetHoursMore180Dashboard(List<TeamsViewModel> items)
        {
            var listAlert = new List<TeamsViewModel>();
            items.Select(i => i.AllHours
               .Where(y => y.Hour > 180)
               .Select(y => {
                   var item = new TeamsViewModel
                   {
                       Hours = y.Hour,
                       Date = y.Date,
                       Name = y.Usersname
                   };
                   listAlert.Add(item);
                   return item;
               }).ToList())
                   .ToList();
            return listAlert;
        }


        public static List<DateTime> DatesBySearch(DateTime from, DateTime to)
        {
            to = to.AddDays(1);
            var dates = new List<DateTime>();
            for (var i = from; i < to; i = i.AddMonths(1))
            {
                dates.Add(i);
            }
            return dates;
        }




        public static List<TeamsViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Teams
                    .Where(i => i.Projects.AccountId == Userservice.AccountId)
                   .OrderBy(i => i.RoleId)
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Teams, TeamsViewModel>();
                        item.RolesName = i.Roles.Name;
                        item.Name = i.Users.Name;
                        return item;
                    }).ToList();
            }
        }



        public static void Save(List<TeamsViewModel> models)
        {
            using (var db = new ShlekenEntities3())
            {
                foreach (var model in models)
                {
                    if (model.Id > 0)
                    {
                        var item = db.Teams.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                        item = model.Map<TeamsViewModel, Teams>(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        
                            var item = model.Map<TeamsViewModel, Teams>();
                            db.Teams.Add(item);
                            db.SaveChanges();
                       
                    }
                }
            }
        }

        public static void Delete(int Projects, int RoleId, int UserId)
        {
            using (var db = new ShlekenEntities3())
            {
                var items = db.Teams.Where(i => i.Projects.AccountId == Userservice.AccountId && i.RoleId == RoleId && i.UserId == UserId && i.ProjectId == Projects).ToList();
                foreach (var item in items)
                {
                    db.Teams.Remove(item);
                    db.SaveChanges();
                }
            }
        }
    }
}
