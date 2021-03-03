using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
  public static   class FixesService
    {
        public static List<FixesViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Fixes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .Map<Fixes, FixesViewModel>();
            }
        }

        public static void Save(List<FixesViewModel> models)
        {
            using (var db = new ShlekenEntities3())
            {
                if (models != null){
                    foreach (var model in models)
                    {
                        if (model.Id > 0)
                        {
                            var item = db.Fixes.SingleOrDefault(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                            if (item != null)
                            {
                                item = model.Map<FixesViewModel, Fixes>(item);
                                db.SaveChanges();
                            }

                        }
                        else
                        {
                            if (model.Name != null)
                            {
                                var item = model.Map<FixesViewModel, Fixes>();
                                db.Fixes.Add(item);
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
                var item = db.Fixes.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Fixes.Remove(item);
                db.SaveChanges();

            }
        }
    }
}
