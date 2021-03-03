using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pajonos.Shleken.Services;
using Pajonos.Shleken.Services.Models;
using Pajonos.Shleken.Web.Models;

namespace Pajonos.Shleken.Web.Controllers
{
    [CheckAuthorization]
    [RoutePrefix("Contacts")]
    public class ProjectsContactsController : Controller
    {
        #region Contacts


        [Route("{Projects}/Contacts")]
        public ActionResult Contacts(int Projects, ContactsSearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(Projects);
            var items = ContactsService.GetAll(Projects, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = Projects;

            return View("~/Views/Project/Contacts.cshtml", items);
        }

        //[Route("{Projects}/Contacts/new")]
        //[HttpGet]
        //public ActionResult ContactsNew(int Projects)
        //{
        //    return View("ContactsEdit", new ContactsViewModel() { ProjectId = Projects });
        //}

        //[Route("{Projects}/Contacts/{id}")]
        //[HttpGet]
        //public ActionResult ContactsEdit(int Projects, int id)
        //{
        //    var item = ContactsService.Single(id);
        //    return View("ContactsEdit", item);
        //}

        //[Route("Contactsave")]
        //[HttpPost]
        //public ActionResult Contactsave(ContactsViewModel model)
        //{
        //    ViewBag.Projects = Projectservice.Get();
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //    ContactsService.Save(model);
        //    return View("CloseAndRefresh");
        //}



        //[Route("{Projects}/Contacts/{id}/delete")]
        //[HttpGet]
        //public ActionResult DeleteContacts(int Projects, int id)
        //{
        //    ViewBag.Projects = Projectservice.Get();
        //    ContactsService.Delete(id);
        //    ViewBag.ProjectId = 1;
        //    return Redirect("~/Projects/" + ViewBag.ProjectId);
        //}
        #endregion
    }
}