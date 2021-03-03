
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Fileservice
    {
        public static List<FilesViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Files
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Take(5)
                    .Map<Files, FilesViewModel>();

            }
        }

        public static List<FilesViewModel> GetAll(int ProjectId, FilesearchViewModel search)
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
                return db.Files
                   .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                    (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower())) &&
                    (search.ToDate == null || i.CreatedDate < search.ToDate) &&
                (search.FromDate == null || i.CreatedDate > search.FromDate)
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Files, FilesViewModel>();
                        return item;
                    })
                    .ToList();
            }

        }


        public static int Add(FilesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = model.Map<FilesViewModel, Files>();
                item.CreatedDate = DateTime.Now;
                item.UserId =7;
                db.Files.Add(item);
                db.SaveChanges();
                return item.Id;
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Files.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Files.Remove(item);
                db.SaveChanges();

            }
        }
    }
}

