using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services
{
    public static class ContactsService
    {
        public static List<ContactsViewModel> Get(int ProjectId)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Contacts
                    .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId)
                    .OrderBy(i=>i.Name)
                    .Take(5)
                    .Map<Contacts, ContactsViewModel>();

            }
        }

        public static List<ContactsViewModel> GetAll(int ProjectId,ContactsSearchViewModel search)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Contacts
                     .Where(i => i.Projects.AccountId == Userservice.AccountId && i.ProjectId == ProjectId &&
                    (string.IsNullOrEmpty(search.Name) == true || i.Name.ToLower().Contains(search.Name.ToLower())) &&
                     (string.IsNullOrEmpty(search.Company) == true || i.Company.ToLower().Contains(search.Company.ToLower())) &&
                    (string.IsNullOrEmpty(search.Phone) == true || i.Phone.ToLower().Contains(search.Phone.ToLower())) 
                    )
                    .ToList()
                    .Select(i =>
                    {
                        var item = i.Map<Contacts, ContactsViewModel>();
                        return item;
                    })
                    .ToList();

            }
        }

        public static ContactsViewModel Single(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                return db.Contacts
                        .Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id)
                        .Map<Contacts, ContactsViewModel>();
            }
        }

        public static void Save(ContactsViewModel model)
        {
            using (var db = new ShlekenEntities3())
            {
                if (model.Id > 0)
                {
                    var item = db.Contacts.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == model.Id);
                    item = model.Map<ContactsViewModel, Contacts>(item);
                    db.SaveChanges();
                }
                else
                {
                    var item = model.Map<ContactsViewModel, Contacts>();
                    db.Contacts.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var db = new ShlekenEntities3())
            {
                var item = db.Contacts.Single(i => i.Projects.AccountId == Userservice.AccountId && i.Id == id);
                db.Contacts.Remove(item);
                db.SaveChanges();

            }
        }
    }
}
