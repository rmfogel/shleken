using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
  public static  class Incomeservice
    {
        public static List<IncomesViewModel> Get(int ProjectId, BudgetSearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Incomes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId&&
                    (search.Cost == 0 || i.Cost == search.Cost) &&
                   (search.ApproverId == 0 || i.ApproverId == search.ApproverId) &&
                   (string.IsNullOrEmpty(search.Description) == true || i.Description.ToLower().Contains(search.Description.ToLower())) 
                     )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Incomes, IncomesViewModel>();
                        item.ApproverName = i.Users.Name;
                        return item;
                    })
                    .ToList();
            }
        }


        public static List<IncomesViewModel> Get(BudgetSearchViewModel search)
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
                return db.Incomes
                    .Where(i => i.Projects.AccountId == Userservice.AccountId &&
                     (search.ProjectId == 0 || i.ProjectId == search.ProjectId) &&
                    (search.ToDate == null || i.Date < search.ToDate) &&
                (search.FromDate == null || i.Date > search.FromDate)
                     )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Incomes, IncomesViewModel>();
                        item.ApproverName = i.Users.Name;
                        return item;
                    })
                    .ToList();
            }
        }

        public static IncomesViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Incomes
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Incomes, IncomesViewModel>();
            }
        }

        public static void Save(IncomesViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Incomes.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<IncomesViewModel, Incomes>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<IncomesViewModel, Incomes>();
                    db.Incomes.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Incomes.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Incomes.Remove(item);
                db.SaveChanges();

            }
        }

        public static int GetIncomesSum()
        {
            return (int)new ShlekenEntities3().Incomes.ToList().Sum(i => i.Cost);
        }

        public static int GetIncomesSum(int project)
        {
            return (int)new ShlekenEntities3().Incomes.Where(p=>p.ProjectId==project).ToList().Sum(i => i.Cost);
        }

       

        public static int GetMounthlyIncomesSum(DateTime date)
        {
            return (int)new ShlekenEntities3().Incomes.Where(i=>i.Date.Month==date.Month&&i.Date.Year==date.Year).ToList().Sum(i => i.Cost);
        }

        public static int GetMounthlyIncomesSum(DateTime date,int project)
        {
            return (int)new ShlekenEntities3().Incomes.Where(p => p.ProjectId == project).ToList().Where(i => i.Date.Month == date.Month && i.Date.Year == date.Year).ToList().Sum(i => i.Cost);
        }

       
    }
}
