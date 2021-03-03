using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Activitieservice
    {
        public static List<ActivitiesViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Activities
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Take(5)
                    .Map<Activities, ActivitiesViewModel>();

            }

        }
  

        public static List<ActivitiesViewModel> GetAll(int ProjectId, ActivitiesearchViewModel search)
        {

            if (search.ToDate != null)
            {
                search.ToDate=search.ToDate.Value.AddDays(1);
            }
            using (var db = new ShlekenEntities3())
            {
                return db.Activities
                   .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                    (string.IsNullOrEmpty(search.Title) == true || i.Title.ToLower().Contains(search.Title.ToLower())) &&
                    (search.ToDate == null || i.Date < search.ToDate) &&
                (search.FromDate == null || i.Date > search.FromDate)
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Activities, ActivitiesViewModel>();
                        return item;
                    })
                    .ToList();

            }

        }

        public static int Add(ActivitiesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = model.Map<ActivitiesViewModel, Activities>();
                item.Date = DateTime.Now;
                item.ProjectId = 1;
                db.Activities.Add(item);
                db.SaveChanges();
                return item.Id;
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Activities.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Activities.Remove(item);
                db.SaveChanges();

            }
        }

        public static object Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Activities
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Activities, ActivitiesViewModel>();
            }
        }

        public static void Save(ActivitiesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Activities.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<ActivitiesViewModel, Activities>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<ActivitiesViewModel, Activities>();
                    item.Date = DateTime.Now;
                    db.Activities.Add(item);
                    db.SaveChanges();
                }
            }
        }
    }
}
