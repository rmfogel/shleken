
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
   public static class Globalservice
    {
        public static List<GlobalsViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Globals
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Map<Globals, GlobalsViewModel>();
            }
        }

        public static void Save(List<GlobalsViewModel> models)
        {
            using (var db = new ShlekenEntities3())
            {
                if (models != null)
                {
                    foreach (var model in models)
                    {
                        if (model.Id > 0)
                        {
                            var item = db.Globals.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                            item = model.Map<GlobalsViewModel, Globals>(item);
                            db.SaveChanges();
                        }
                        else
                        {
                            if (model.Name != null)
                            {
                                var item = model.Map<GlobalsViewModel, Globals>();
                                db.Globals.Add(item);
                                db.SaveChanges();
                            }

                        }
                    }
                }
               
               
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Globals.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Globals.Remove(item);
                db.SaveChanges();

            }
        }
    }
}
