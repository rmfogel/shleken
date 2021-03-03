using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Meetingservice
    {
        public static List<MeetingsViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Meetings
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                     .OrderByDescending(i=> Math.Abs((DateTime.Now.Month - i.Date.Month) + (12 * (DateTime.Now.Year - i.Date.Year))))
                     .Take(5)
                  .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Meetings, MeetingsViewModel>();
                        return item;
                    })
                    .ToList();
            }
        }

        public static List<MeetingsViewModel> GetAll(int ProjectId, MeetingsearchViewModel search)
        {
            if (search.ToDate != null)
            {
                search.ToDate = search.ToDate.Value.AddDays(1);
            }
            if (search.FromDate != null)
            {
                search.FromDate = search.FromDate.Value.AddDays(-1);
            }
            using (var db = new ShlekenEntities3())
            {
                return db.Meetings
                   .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                    (string.IsNullOrEmpty(search.Subject) == true || i.Subject.ToLower().Contains(search.Subject.ToLower())) &&
                      (string.IsNullOrEmpty(search.Location) == true || i.Location.ToLower().Contains(search.Location.ToLower())) &&
                    (search.ToDate == null || i.Date < search.ToDate) &&
                (search.FromDate == null || i.Date > search.FromDate)
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Meetings, MeetingsViewModel>();
                        return item;
                    })
                    .ToList();
            }

        }


        //public static int Add(MeetingsViewModel model)
        //{
        //    using (var db = new ShlekenEntities3())
        //    {
        //        var item = model.Map<MeetingsViewModel, Meetings>();
        //        item.UserId = Userservice.UserId;
        //        item.Subject = "statu1";

        //        item.ProjectId = 1;
        //        db.Meetings.Add(item);
        //        db.SaveChanges();
        //        return item.Id;
        //    }
        //}

        //public static void Delete(int id)
        //{
        //    using (var db = new ShlekenEntities3())
        //    {
        //        var item = db.Meetings.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
        //        db.Meetings.Remove(item);
        //        db.SaveChanges();
        //    }
        //}
        public static MeetingsViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Meetings
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Meetings, MeetingsViewModel>();
            }
        }

        public static void Save(MeetingsViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Meetings.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<MeetingsViewModel, Meetings>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<MeetingsViewModel, Meetings>();
                    item.UserId = 7;

                    db.Meetings.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Meetings.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Meetings.Remove(item);
                db.SaveChanges();

            }
        }
    }
}
