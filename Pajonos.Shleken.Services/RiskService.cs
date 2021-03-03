using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class Riskservice
    {
        public static List<RisksViewModel> Get(int ProjectId, RisksearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Risks
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                    (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower())) &&
                     (search.Status == 0 || i.Status == search.Status) &&
                      (search.Priority == 0 || i.Priority == search.Priority) &&
                      (search.Probabilty == 0 || i.Probabilty == search.Probabilty) &&
                    (search.UserId == 0 || i.UserId == search.UserId)
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Risks, RisksViewModel>();
                        item.ProbabiltyName = i.RiskProbabilities.Name;
                        item.PriorityName = i.RiskPriorities.Name;
                        item.StatusName = i.RiskStatuses.Name;
                        item.UsersName = i.Users.Name;
                        return item;
                    })
                    .ToList().OrderBy(i=>i.Date).ToList();
            }
        }

        public static RisksViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Risks
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Risks, RisksViewModel>();
            }
        }

        public static void Save(RisksViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Risks.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<RisksViewModel, Risks>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<RisksViewModel, Risks>();
                    db.Risks.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Risks.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Risks.Remove(item);
                db.SaveChanges();

            }
        }
        //public static RisksViewModel GetNextRisks(int ProjectId)
        //{
        //    using (var db = new ShlekenEntities3())
        //    {
        //        var closestDate = db.Risks.Where(i => i.Date > DateTime.Now&&i.Projects.Id== ProjectId&&i.Projects.AccountId==Userservice.AccountId)
        //            .OrderBy(i => i.Date)
        //            .First();
           
        //        var item = closestDate.Map<Risks, RisksViewModel>();
        //        item.ProbabiltyName = closestDate.RiskProbabilities.Name;
        //        item.PriorityName = closestDate.RiskPriorities.Name;
        //        item.StatusName = closestDate.RiskStatuses.Name;
        //        item.UsersName = closestDate.Users.Name;
        //        return item;
               
        //    }
        //}

    }
}
