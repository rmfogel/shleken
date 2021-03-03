using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class ToDoListservice
    {
        public static List<ToDoListsViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.ToDoLists
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .OrderByDescending(i => i.CreatedDate).Take(5)
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<ToDoLists, ToDoListsViewModel>();
                        item.Status = i.ToDoListStatuses.Name;
                        return item;
                    })
                    .ToList();

            }
        }

        public static List<ToDoListsViewModel> GetAll(int ProjectId, ToDoListsearchViewModel search)
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
                return db.ToDoLists
                     .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                      (search.ToDate == null || i.Date < search.ToDate) &&
                      (search.FromDate == null || i.Date > search.FromDate) &&
                      (search.IsDone == 0 || i.IsDone == search.IsDone) &&
                      (string.IsNullOrEmpty(search.Description) == true || i.Description.ToLower().Contains(search.Description.ToLower()))
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<ToDoLists, ToDoListsViewModel>();
                        item.Status = i.ToDoListStatuses.Name;
                        return item;
                    })
                    .ToList();
            }
        }





        //public static int Add(ToDoListsViewModel model)
        //{
        //    using (var db = new ShlekenEntities3())
        //    {
        //        var item = model.Map<ToDoListsViewModel, ToDoLists>();
        //        item.CreatedDate = DateTime.Now;
        //        item.ProjectId = 1;
        //        item.UserId = 7;
        //        db.ToDoLists.Add(item);
        //        db.SaveChanges();
        //        return item.Id;
        //    }
        //}

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.ToDoLists.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.ToDoLists.Remove(item);
                db.SaveChanges();
            }
        }

        public static object Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.ToDoLists.
                     Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id).
                     Map<ToDoLists, ToDoListsViewModel>();
            }
        }

        public static void Save(ToDoListsViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.ToDoLists.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<ToDoListsViewModel, ToDoLists>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<ToDoListsViewModel, ToDoLists>();
                    item.CreatedDate = DateTime.Now;
                    item.Userid = Userservice.UserId;
                    db.ToDoLists.Add(item);
                    db.SaveChanges();
                }
            }
        }
    }
}
