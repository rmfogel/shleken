using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
   public static class Outcomeservice
    {
        public static List<OutcomesViewModel> Get(int ProjectId, OutcomesearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Outcomes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                   (search.Cost == 0 || i.Cost == search.Cost) &&
                  (search.ApproverId == 0 || i.ApproverId == search.ApproverId) &&
                     (string.IsNullOrEmpty(search.Description) == true || i.Description.ToLower().Contains(search.Description.ToLower()))
                     )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Outcomes, OutcomesViewModel>();
                        item.ApproverName = i.Users.Name;
                        return item;
                    })
                    .ToList();
            }
        }


        public static List<OutcomesViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Outcomes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId 
                
                     )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Outcomes, OutcomesViewModel>();
                        item.ApproverName = i.Users.Name;
                        return item;
                    })
                    .ToList();
            }
        }


        public static List<OutcomesViewModel> Get(BudgetSearchViewModel search)
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
                return db.Outcomes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId &&
                     (search.ProjectId == 0 || i.ProjectId == search.ProjectId) &&
                    (search.ToDate == null || i.Date < search.ToDate) &&
                     (search.FromDate == null || i.Date > search.FromDate)
                     )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Outcomes, OutcomesViewModel>();
                        item.ApproverName = i.Users.Name;
                        return item;
                    })
                    .ToList();
            }
        }

        public static OutcomesViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Outcomes
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Outcomes, OutcomesViewModel>();
            }
        }

        public static void Save(OutcomesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Outcomes.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<OutcomesViewModel, Outcomes>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<OutcomesViewModel, Outcomes>();
                    db.Outcomes.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Outcomes.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Outcomes.Remove(item);
                db.SaveChanges();

            }
        }

        public static int GetOutcomesSum()
        {
            return (int)new ShlekenEntities3().Outcomes.ToList().Sum(i => i.Cost);
        }

        public static int GetOutcomesSum(int project)
        {
            return (int)new ShlekenEntities3().Outcomes.Where(p => p.ProjectId == project).ToList().Sum(i => i.Cost);
        }

        public static int GetMounthlyOutcomesSum(DateTime date)
        {
            return (int)new ShlekenEntities3().Outcomes.Where(i => i.Date.Month == date.Month && i.Date.Year == date.Year).ToList().Sum(i => i.Cost);
        }

        public static int GetMounthlyOutcomesSum(DateTime date, int project)
        {
            return (int)new ShlekenEntities3().Outcomes.Where(p => p.ProjectId == project).ToList().Where(i => i.Date.Month == date.Month && i.Date.Year == date.Year).ToList().Sum(i => i.Cost);
        }
    }
}
