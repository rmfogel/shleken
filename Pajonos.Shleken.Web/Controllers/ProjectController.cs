using Pajonos.Shleken.Services;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;
using Pajonos.Shleken.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pajonos.Shleken.Web.Controllers
{
    [CheckAuthorization]
    [RoutePrefix("Project")]
    public class ProjectController : Controller
    {


        [Route("{Project}")]
        //        public async System.Threading.Tasks.Tasks<ActionResult> IndexAsync(int project)
        public ActionResult Index(int Projects)
        {
            Session["CurrentProId"] = Projects;
            var Outcomes = Outcomeservice.Get(Projects);
            //Taskservice.Save(listTasks);
          var jira =  JiraService.GetJira(Projects);
          ViewBag.Jira = jira;
            ViewBag.Projects = Projectservice.Get(Projects);

            ViewBag.OutcomesUntilNow = Outcomes.Sum(i => i.Cost);
            ViewBag.OuotcomeDiff = ViewBag.Projects.TaskTotalCost - Outcomes.Sum(i => i.Cost);
            var percentage = (ViewBag.OuotcomeDiff / ViewBag.Projects.TaskTotalCost) * 100;
            percentage = Math.Floor(percentage);
            ViewBag.Percenses = percentage;
            ViewBag.ProjectsName = Projectservice.GetnameProjects(Projects);

            ViewBag.Status = ToDoListStatusesService.Get();
            ViewBag.Projects = Projectservice.Get();
            //var closestRisks = Riskservice.(Projects);
            //ViewBag.Days = Enumerable.Range(0, 1 + (closestRisks.Date - DateTime.Now).Days)
//.Select(offset => (DateTime.Now).AddDays(offset)).Count();
            var percentageSprint = jira.ClosedIssuesInActiveSprint / ViewBag.Jira.AllIssuesInActiveSprint * 100;
            if (percentageSprint > 0)
            {
                ViewBag.PercentageSprint = percentageSprint;
            }
            else
            {
                ViewBag.PercentageSprint = 0;
            }
            var percentageCloseSprint = jira.ClosestSprint * 100 / jira.CountSprint;
            if (percentageCloseSprint > 0)
            {
                ViewBag.PercentageCloseSprint = percentageCloseSprint;
            }
            else
            {
                ViewBag.PercentageCloseSprint = 0;
            }
            //ViewBag.ClosestRisks = closestRisks;
            ViewBag.ProjectId = Projects;
            ViewBag.CurrentProject = Projectservice.Get(Projects);
            Session["isManneger"] = Userservice.IsProjectManager(Projects, ((UsersViewModel)Session["CurrentUsers"]).Id);
            return View();
        }

        #region Contacts
        [Route("{project}/Contacts")]
        public ActionResult Contacts(int project, ContactsSearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var items = ContactsService.GetAll(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;

            return View("~/Views/project/Contacts.cshtml", items);
        }


        //[Route("{project}/Contacts")]
        //public ActionResult Contacts(int project, ContactsSearchViewModel search)
        //{
        //    ViewBag.ProjectsName = Projectservice.GetnameProjects(Projects);
        //    var items = ContactsService.GetAll(Projects, search);
        //    ViewBag.Search = search;
        //    ViewBag.ProjectId = Projects;
        //    return View(items);
        //}
     
        [Route("{project}/Contacts/new")]
        [HttpGet]
        public ActionResult ContactsNew(int project)
        {
            return View("ContactsEdit", new ContactsViewModel() { ProjectId = project });
        }

       [Route("{project}/Contacts/{id}")]
        [HttpGet]
        public ActionResult ContactsEdit(int project, int id)
        {
            var item = ContactsService.Single(id);
            return View("ContactsEdit", item);
        }

        [Route("Contactsave")]
        [HttpPost]
        public ActionResult Contactsave(ContactsViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            ContactsService.Save(model);
            return View("CloseAndRefresh");
        }



        [Route("{project}/Contacts/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteContacts(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            ContactsService.Delete(id);
            ViewBag.ProjectId = project;
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }
# endregion

        #region Meetings
        [Route("{project}/Meetings")]
        public ActionResult Meetingsummery(int project, MeetingsearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var items = Meetingservice.GetAll(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            return View(items);
        }


        [Route("{project}/Meetings/new")]
        [HttpGet]
        public ActionResult MeetingsNew(int project)
        {
            return View("MeetingsEdit", new MeetingsViewModel() { ProjectId = project, Date = DateTime.Now });
        }

        [Route("{project}/Meetings/{id}")]
        [HttpGet]
        public ActionResult MeetingsEdit(int project, int id)
        {
            var item = Meetingservice.Single(id);
            return View("MeetingsEdit", item);
        }

        [Route("Meetingsave")]
        [HttpPost]
        public ActionResult Meetingsave(MeetingsViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            Meetingservice.Save(model);
            return View("CloseAndRefresh");
        }



        [Route("{project}/Meetings/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteMeetings(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Meetingservice.Delete(id);
            ViewBag.ProjectId = project;
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }
        #endregion

        #region Risks

        [Route("{project}/Risks")]
        public ActionResult Risks(int project, RisksearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var UsersSearch = Userservice.Get();
            UsersSearch.Insert(0, new UsersViewModel() { Id = 0, Name = "All" });
            ViewBag.UsersSearch = UsersSearch;
            var items = Riskservice.Get(project, search);
            var Users = Userservice.Get();
            var priority = RiskPrioritiesService.Get();
            priority.Insert(0, new RiskPrioritiesViewModel() { Id = 0, Name = "All" });
            ViewBag.Priority = priority;

            var probability = RiskProbabilitiesService.Get();
            probability.Insert(0, new RiskProbabilitiesViewModel() { Id = 0, Name = "All" });
            ViewBag.Probability = probability;

            var status = RiskStatusesService.Get();
            status.Insert(0, new RiskStatusesViewModel() { Id = 0, Name = "All" });
            ViewBag.Status = status;
            ViewBag.RisksTotal = items.Sum(i => i.Cost);
            ViewBag.Users = Users;

            ViewBag.Risks = Riskservice.Get(project, search);
            ViewBag.Projects = Projectservice.Get();
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            return View(items);
        }



        [Route("{project}/Risks/new")]
        [HttpGet]
        public ActionResult RisksNew(int project)
        {
            var Users = Userservice.Get();
            ViewBag.Status = RiskStatusesService.Get();
            ViewBag.Probability = RiskProbabilitiesService.Get();
            ViewBag.Priority = RiskPrioritiesService.Get();
            ViewBag.Users = Users;
            return View("RisksEdit", new RisksViewModel() { ProjectId = project, UserId = Userservice.UserId, Date = DateTime.Now });
        }

        [Route("{project}/Risks/{id}")]
        [HttpGet]
        public ActionResult RisksEdit(int project, int id)
        {
            ViewBag.Status = RiskStatusesService.Get();
            ViewBag.Probability = RiskProbabilitiesService.Get();
            ViewBag.Priority = RiskPrioritiesService.Get();
            var Users = Userservice.Get();
            ViewBag.Users = Users;
            var item = Riskservice.Single(id);
            return View("RisksEdit", item);
        }

        [Route("RiskstSave")]
        [HttpPost]
        public ActionResult RiskstSave(RisksViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            Riskservice.Save(model);
            return View("CloseAndRefresh");
        }

        [Route("{project}/Risks/{id}/delete")]
        [HttpGet]
        public ActionResult RisksDelete(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Riskservice.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + ViewBag.ProjectId + "/Risks");
        }
        #endregion

        #region Outcomes
        [Route("{project}/Outcomes/new")]
        [HttpGet]
        public ActionResult OutcomesNew(int project)
        {
            var Users = Userservice.Get();

            ViewBag.Users = Users;
            return View("OutcomesEdit", new OutcomesViewModel() { ProjectId = project, Date = DateTime.Now });
        }

        [Route("{project}/Outcomes/{id}")]
        [HttpGet]
        public ActionResult OutcomesEdit(int project, int id)
        {
            var Users = Userservice.Get();
            ViewBag.Users = Users;
            var item = Outcomeservice.Single(id);
            return View("OutcomesEdit", item);
        }

        [Route("Outcomesave")]
        [HttpPost]
        public ActionResult Outcomesave(OutcomesViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            Outcomeservice.Save(model);
            return View("CloseAndRefresh");
        }

        [Route("{project}/Outcomes/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteOutcomes(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Outcomeservice.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + ViewBag.ProjectId + "/budget");
        }
        #endregion





        [Route("{project}/timesheet")]
        public ActionResult Timesheet(int project)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            ViewBag.Projects = Projectservice.Get();
            ViewBag.ProjectId = project;
            return View();
        }

        [Route("{project}/budget")]
        public ActionResult Budget(int project)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var Incomes = new BudgetSearchViewModel();
            var Outcomes = new OutcomesearchViewModel();
            ViewBag.ProjectId = project;
            var incom = Incomeservice.Get(project, Incomes);
            var outcom = Outcomeservice.Get(project, Outcomes);

            var items = new BudgetViewModel()
            {
                Incomes = incom,
                Outcomes = outcom,
                IncomesTotal = incom.Sum(i => i.Cost),
                OutcomesTotal = outcom.Sum(i => i.Cost),
            };

            var Users = Userservice.Get();
            Users.Insert(0, new UsersViewModel() { Id = 0, Name = "All" });

            ViewBag.Users = Users;
            //ViewBag.Incomes = Incomes;
            //ViewBag.Outcomes = Outcomes;
            if (((bool)Session["isManneger"]) == true)
                return View(items);
            return View("~/Views/Shared/NoPermitions.cshtml");
           
        }

        [Route("{project}/statistics")]
        public ActionResult Statistics(int project)
        {


            var Users = Userservice.Get().ToList();
            List<int> Hours = new List<int>();
            List<int> Status = new List<int>();
            List<int> incomes = new List<int>();
            List<int> outcomes = new List<int>();
            Status.Add(Taskservice.GetNumOfProjectStatuses("DONE",project));
            Status.Add(Taskservice.GetNumOfProjectStatuses("in progress", project));
            Status.Add(Taskservice.GetNumOfProjectStatuses("new", project));
            ViewBag.Status = Status;
            for (int i = 0; i < 12; i++)
            {
                DateTime date = new DateTime(DateTime.Today.Year, i + 1, 1);
                Hours.Add(Teamservice.GetHoursForMonth(date,project));
                incomes.Add(Incomeservice.GetMounthlyIncomesSum(date,project));
                outcomes.Add(Outcomeservice.GetMounthlyOutcomesSum(date,project));
            }
            ViewBag.Hours = Hours;
            ViewBag.Incomes = incomes;
            ViewBag.Outcomes = outcomes;


            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            ViewBag.Project = Projectservice.Get(project);
            ViewBag.UserTasks = Teamservice.Get(project).Select(i => new {i.Users.Name,RoleName=i.Roles.Name,i.AllHours }).ToList();
            ViewBag.Projects = Projectservice.Get();
            ViewBag.ProjectId = project;
            ViewBag.Users = Userservice.Get(project).Select(i=>i.Name).ToList();
            return View();
        }

        [Route("{project}/Settings")]
        [HttpGet]
        public ActionResult Settings(int project)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var Users = Userservice.Get();
            ViewBag.Users = Users;
            ViewBag.Projects = Projectservice.Get();
            ViewBag.ProjectId = project;
            ViewBag.Status = ProjectStatusesService.Get();
            var item = Projectservice.Single(project);
            return View(item);
        }

        [Route("Settings")]
        [HttpPost]
        public ActionResult Settings(ProjectsViewModel model)
        {
            //if (ModelState.IsValid == false)
            //    return View(model);

            ViewBag.Projects = Projectservice.Get();
            ViewBag.ProjectId = model.Id;
            Projectservice.Save(model);
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }

        //[Route("Projects/{id}")]
        //[HttpGet]
        //public ActionResult ProjectsEdit(int id)
        //{
        //    var item = Projectservice.Single(id);
        //    return View("ProjectsEdit", item);
        //}

        #region Incomes


        [Route("{project}/Incomes/new")]
        [HttpGet]
        public ActionResult IncomestNew(int project)
        {
            var Users = Userservice.Get();
            ViewBag.Users = Users;
            return View("IncomesEdit", new IncomesViewModel() { ProjectId = project, Date = DateTime.Now });
        }

        [Route("{project}/Incomes/{id}")]
        [HttpGet]
        public ActionResult IncomesEdit(int project, int id)
        {
            var Users = Userservice.Get();
            ViewBag.Users = Users;
            var item = Incomeservice.Single(id);
            return View("IncomesEdit", item);
        }

        [Route("Incomesave")]
        [HttpPost]
        public ActionResult Incomesave(IncomesViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            Incomeservice.Save(model);
            return View("CloseAndRefresh");
        }



        [Route("{project}/Incomes/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteIncomes(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Incomeservice.Delete(id);
            ViewBag.ProjectId = 1;
            return Redirect("~/project/" + ViewBag.ProjectId + "/budget");
        }
        #endregion

        [Route("CreateContacts")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContacts([Bind(Prefix  = "ContactsViewModel")]ContactsViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            ContactsService.Save(model);
            return RedirectToAction("Index", new { Projects = model.ProjectId });
        }

        //[Route("CreateMeetings")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateMeetings([Bind(PreFixes = "MeetingsViewModel")]MeetingsViewModel model)
        //{
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //    Meetingservice.Add(model);
        //    return Redirect("~/projects/" + model.ProjectId);
        //}

        #region Files
        [Route("{project}/Files")]
        public ActionResult Files(int project, FilesearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var items = Fileservice.GetAll(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            return View(items);
        }

        [Route("{project}/Files/new")]
        [HttpGet]
        public ActionResult FilesNew(int project)
        {
            return View("FilesEdit", new FilesViewModel() { ProjectId = project });
        }


        [Route("Filesave")]
        [HttpPost]
        public ActionResult Filesave(FilesViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            Fileservice.Add(model);
            return View("CloseAndRefresh");
        }


        [Route("{project}/Files/{id}/delete")]
        public ActionResult DeleteFiles(int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Fileservice.Delete(id);
            ViewBag.ProjectId = Session["CurrentProId"];
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }
        #endregion

        #region ToDoLists
        [Route("{project}/ToDoLists")]
        public ActionResult ToDoLists(int project, ToDoListsearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var status = ToDoListStatusesService.Get();
            status.Insert(0, new ToDoListStatusesViewModel() { Id = 0, Name = "All" });
            ViewBag.Status = status;
            var items = ToDoListservice.GetAll(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            return View(items);
        }

        [Route("{project}/ToDoLists/new")]
        [HttpGet]
        public ActionResult ToDoListsNew(int project)
        {
            ViewBag.Status = ToDoListStatusesService.Get();
            return View("ToDoListsEdit", new ToDoListsViewModel() { ProjectId = project, Date = DateTime.Now });
        }

        [Route("{project}/ToDoLists/{ToDoLists}")]
        [HttpGet]
        public ActionResult ToDoListsEdit(int project, int ToDoLists)
        {
            ViewBag.Status = ToDoListStatusesService.Get();
            var item = ToDoListservice.Single(ToDoLists);
            return View("ToDoListsEdit", item);
        }

        [Route("ToDoListsave")]
        [HttpPost]
        public ActionResult ToDoListsave(ToDoListsViewModel model)
        {
            ViewBag.Projects = Projectservice.Get();
            if (ModelState.IsValid == false)
                return View(model);

            ToDoListservice.Save(model);
            return View("CloseAndRefresh");
        }

        //[HttpPost]
        //public ActionResult ToDoListsChange(string accountNumber)
        //{
        //    //Of course you want to authorize the call

        //    var db = new BankEntities();
        //    var a = db.Accounts.FirstOrDefault(x => x.account_number == accountNumber);
        //    if (a != null)
        //    {
        //        return Json(new { Status = "success", Balance = a.Balance });
        //    }
        //    else
        //    {
        //        return Json(new { Status = "error" });
        //    }
        //}

        [Route("{project}/ToDoLists/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteToDoLists(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            ToDoListservice.Delete(id);
            ViewBag.ProjectId = project;
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });

        }

        #endregion

        #region Activities

        [Route("{project}/activities")]
        public ActionResult Activities(int project, ActivitiesearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var items = Activitieservice.GetAll(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            return View(items);
        }


        [Route("{project}/activities/new")]
        [HttpGet]
        public ActionResult ActivitiesNew(int project)
        {
            return View("ActivitiesEdit", new ActivitiesViewModel() { ProjectId = project });
        }

        [Route("{project}/activities/{Activities}")]
        [HttpGet]
        public ActionResult ActivitiesEdit(int project, int Activities)
        {
            var item = Activitieservice.Single(Activities);
            return View("ActivitiesEdit", item);
        }


        [Route("Activitiesave")]
        [HttpPost]
        public ActionResult Activitiesave(ActivitiesViewModel model)
        {

            if (ModelState.IsValid == false)
                return View(model);

            Activitieservice.Save(model);
            return View("CloseAndRefresh");
        }

        [Route("{project}/activities/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteActivities(int project, int id)
        {

            Activitieservice.Delete(id);
            ViewBag.ProjectId = project;
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }
        #endregion

        #region Teams
        [Route("{project}/Teams")]
        public ActionResult Teams(int project)
        {
            var hours = false;
            var cost = false;
            Projectservice.GetHoursCost(project, ref hours, ref cost);
            ViewBag.Hours = hours;
            ViewBag.Cost = cost;
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var Users = Userservice.Get();
            ViewBag.Active = true;
            var items = Teamservice.GetHoursUsers(project);
            ViewBag.AlertMore180Hours = Teamservice.GetHoursMore180(items, project);
            ViewBag.Users = Users;
            ViewBag.DatesProjects = Teamservice.DatesProjects(project);
            var Roles = RolesService.Get(project);
            ViewBag.Roles = Roles;
            ViewBag.ProjectsDate = Projectservice.Single(project);
            ViewBag.ProjectId = project;
            return View(items);
        }


        [Route("Teamsave")]
        [HttpPost]
        public void Teamsave(List<TeamsViewModel> listTeams, int TeamTotalHours, int TeamTotalCost, int ProjectId)
        {

            Teamservice.Save(listTeams);
            Projectservice.SaveTeamsHoursCost(TeamTotalHours, TeamTotalCost, ProjectId);
            //  return Redirect("~/projects/" + model.ProjectId + "/Teams");
        }

        [Route("GetDatesProjects/{project}")]
        [HttpGet]
        public ActionResult GetDatesProjects(int project)
        {
            ViewBag.ProjectId = project;
            var data = Teamservice.DatesProjects(ViewBag.ProjectId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Route("{project}/Teams/{RoleId}/{UserId}/delete")]
        public ActionResult DeleteFiles(int project, int RoleId, int UserId)
        {
            ViewBag.ProjectId = project;
            Teamservice.Delete(project, RoleId, UserId);
            return Redirect("~/project/" + ViewBag.ProjectId + "/Teams");
        }
        #endregion

        #region Roles
        [Route("CreateRoles")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoles([Bind(Prefix = "RolesViewModel")]RolesResourcesViewModel model,int User)
        {
            var Roles = RolesService.Get(model.ProjectId);
            ViewBag.UserRoles = Userservice.GetRefUsers(model.ProjectId);

            ViewBag.Tasks = Taskservice.Get(model.ProjectId).Where(t=>t.Status!="done").ToList();
            ViewBag.NotActiveTasks = Taskservice.Get(model.ProjectId).Where(t => t.Status == "done").ToList();
            ViewBag.Fixes = FixesService.Get(model.ProjectId);
            ViewBag.Globals = Globalservice.Get(model.ProjectId);
            ViewBag.Roles = RolesService.Get(model.ProjectId);
            ViewBag.ProjectsName = Projectservice.GetnameProjects(model.ProjectId);
            ViewBag.Projects = Projectservice.Get();
            ViewBag.Users = Userservice.Get();

            ViewBag.ShowModel = 1;
            ViewBag.ProjectId = model.ProjectId;
           
            if (ModelState.IsValid == false)

                return Redirect("~/project/" + model.ProjectId + "/Tasks");


            RolesService.AddRoleForUser(model,User);

            return Redirect("~/project/" + model.ProjectId + "/Tasks");
        }

        [Route("{project}/Roles/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteRoles(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            RolesService.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + project+ "/Tasks");
        }

        #endregion
        #region Tasks
        [Route("{project}/Tasks")]
        public ActionResult Tasks(int project)
        {
            ViewBag.Users = Userservice.Get();
            ViewBag.Statuses = ProjectStatusesService.Get();
            var p = Taskservice.Get(project);
            ViewBag.Tasks = Taskservice.Get(project).Where(t => t.Status != "done").ToList();
            ViewBag.NotActiveTasks = Taskservice.Get(project).Where(t => t.Status == "done").ToList();
            ViewBag.Fixes= FixesService.Get(project);
            ViewBag.Globals = Globalservice.Get(project);
            ViewBag.Roles= Userservice.Get(project);
            ViewBag.UserRoles = Userservice.GetRefUsers(project);
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            ViewBag.Projects = Projectservice.Get();
            
            ViewBag.Current = Projectservice.Get(project);
            ViewBag.ProjectId = project;
            ViewBag.ShowModel = 0;
            return View();
        }

       [Route("Tasksave")]
        [HttpPost]
        public void Tasksave(List<TasksViewModel> listTasks, int TaskTotalHours, int TaskTotalCosts, int ProjectId)
        {
            Taskservice.Save(listTasks);
            Projectservice.SaveTasksHoursCost(TaskTotalHours, TaskTotalCosts, ProjectId);
            //   return TaskId;
        }



        [Route("GetRoles/{project}")]
        [HttpGet]
        public ActionResult GetRoles(int project)
        {
            ViewBag.ProjectId = project;
            var data = RolesService.Get(ViewBag.ProjectId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [Route("{project}/Tasks/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteResources(int project, int id)
        {

            Taskservice.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + ViewBag.ProjectId + "/Tasks");
        }
        #endregion

        #region Fixes

        [Route("FixesSave")]
        [HttpPost]
        public void FixesSave(List<FixesViewModel> model)
        {
            ViewBag.Projects = Projectservice.Get();
            FixesService.Save(model);
            //  return Redirect("~/project/" + model.ProjectId + "/Tasks");
        }

        [Route("{project}/Fixes/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteFixes(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            FixesService.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + ViewBag.ProjectId + "/Tasks");
        }

        #endregion

        #region Globals
        [Route("Globalsave")]
        [HttpPost]
        public void Globalsave(List<GlobalsViewModel> model)
        {
            ViewBag.Projects = Projectservice.Get();
            Globalservice.Save(model);
            // return Redirect("~/project/" + model.ProjectId + "/Tasks");
        }

        [Route("{project}/Globals/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteGlobals(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Globalservice.Delete(id);
            ViewBag.ProjectId = project;
            return Redirect("~/project/" + ViewBag.ProjectId + "/Tasks");
        }
        #endregion


        //[Route("CreateIncomes")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateIncomes([Bind(PreFixes = "IncomesViewModel")]IncomesViewModel model)
        //{
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //    Incomeservice.Add(model);
        //    model.ProjectId = 1;
        //    return Redirect("~/project/" + model.ProjectId + "/budget");
        //}

        //[Route("CreateOutcomes")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateOutcomes([Bind(PreFixes = "OutcomesViewModel")]OutcomesViewModel model)
        //{
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //    Outcomeservice.Add(model);
        //    model.ProjectId = 1;
        //    return Redirect("~/project/" + model.ProjectId + "/budget");
        //}



        //[Route("DeleteMeetingsummery")]
        //public ActionResult DeleteMeetingsummery(int id)
        //{
        //    ViewBag.ProjectId = 1;
        //    Meetingservice.Delete(id);
        //    return Redirect("~/project/" + ViewBag.ProjectId);
        //}





        //[Route("DeleteRisks")]
        //public ActionResult DeleteRisks(int id)
        //{
        //    ViewBag.ProjectId = 1;
        //    Riskservice.Delete(id);
        //    return Redirect("~/project/" + ViewBag.ProjectId + "/Risks");
        //}

        //[Route("DeleteIncomes")]
        //public ActionResult DeleteIncomes(int id)
        //{
        //    ViewBag.ProjectId = 1;
        //    Incomeservice.Delete(id);
        //    return Redirect("~/project/" + ViewBag.ProjectId);
        //}

        //[Route("DeleteOutcomes")]
        //public ActionResult DeleteOutcomes(int id)
        //{
        //    ViewBag.ProjectId = 1;
        //    Incomeservice.Delete(id);
        //    return Redirect("~/project/" + ViewBag.ProjectId);
        //}





        #region Users
        [Route("{project}/Users")]
        public ActionResult Users(int project, UsersSearchViewModel search)
        {
            ViewBag.ProjectsName = Projectservice.GetnameProjects(project);
            var users = Userservice.Get(project);
            users.Insert(0, new UsersViewModel() { Id = 0, Name = "All" });
            ViewBag.Users = User;
            var items = Userservice.Get(project, search);
            ViewBag.Search = search;
            ViewBag.ProjectId = project;
            if(((bool)Session["isManneger"])==true)
            return View(items);
            return View("~/Views/Shared/NoPermitions.cshtml");
        }

        [Route("{project}/Users/new")]
        [HttpGet]
        public ActionResult UsersNew(int project)
        {
            @ViewBag.ProjectId = project;
            ViewBag.Roles = RolesService.Get(project);
            RefUserProViewModel ru = new RefUserProViewModel { ProjecrId = project, Task = new Services.Entities.TasksRoles { Roles = new Services.Entities.Roles(), Tasks = new Services.Entities.Tasks { } } };
            return View("UsersEdit",ru );
        }

        [Route("{project}/Users/{Users}")]
        [HttpGet]
        public ActionResult Users(int project, int Users)
        {
            ViewBag.Status = ToDoListStatusesService.Get();
            var item = Userservice.Single(Users,project);
            Session["user"] = item;
            return View("UsersEdit", item);
        }

        [Route("Userssave")]
        [HttpPost]
        public ActionResult Userssave(RefUserProViewModel model,string RoleName,int Value, HttpPostedFileBase Image)
        {
            ViewBag.Projects = Projectservice.Get();
            //if (ModelState.IsValid == false)
            //    return View(model);
            if (Session["user"] == null)
            {
                RolesService.New(model.ProjectId, RoleName);
                model.Task = new Services.Entities.TasksRoles { RoleId = RolesService.GetLast() };
            }
            else
            {
                var model2 = (RefUserProViewModel)Session["user"];
                model2.Users.Name = model.Name;
                model2.Users.Password = model.Password;
                model2.Users.Email = model.Email;
                model2.Value = Value;
            }


            var path = Path.Combine(Server.MapPath("~/images/"),RoleName + ".png");
            if(Image!=null)
            Image.SaveAs(path);
            Userservice.Save(model);
            return View("CloseAndRefresh");
        }

        //[HttpPost]
        //public ActionResult ToDoListsChange(string accountNumber)
        //{
        //    //Of course you want to authorize the call

        //    var db = new BankEntities();
        //    var a = db.Accounts.FirstOrDefault(x => x.account_number == accountNumber);
        //    if (a != null)
        //    {
        //        return Json(new { Status = "success", Balance = a.Balance });
        //    }
        //    else
        //    {
        //        return Json(new { Status = "error" });
        //    }
        //}

        [Route("{project}/Users/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteUsers(int project, int id)
        {
            ViewBag.Projects = Projectservice.Get();
            Userservice.Delete(id,project);
            ViewBag.ProjectId = project;
            return RedirectToAction("Index", new { Projects = ViewBag.ProjectId });
        }

        #endregion



    }
}