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
    public class RootController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Projects = Projectservice.Get();
            return View();
        }

        [Route("Projects")]
        public ActionResult Projects(ProjectsSearchViewModel search)
        {
            ViewBag.Projects = Projectservice.Get();
            var items = Projectservice.Get(search,((UsersViewModel)Session["CurrentUsers"]).Id).Where(p => p.StatusName != "disabled").ToList().ToList();
            var Users = Userservice.Get();
            Users.Insert(0, new UsersViewModel() { Id = 0, Name = "All" });
            var u = Projectservice.Get(search);
            ViewBag.Users = Users;
            var status = ProjectStatusesService.Get();
            status.Insert(0, new ProjectStatusesViewModel() { Id = 0, Name = "All" });
            ViewBag.Status = status;
            ViewBag.Search = search;

            return View(items);
        }







        [Route("Teams")]
        public ActionResult Teams(TeamsearchViewModel search)
        {
            // ViewBag.DatesProjects = Teamservice.DatesProjects();

            var Projects = Projectservice.Get();
            Projects.Insert(0, new ProjectsViewModel() { Id = 0, Name = "All" });
            ViewBag.Projects = Projects;
            ViewBag.Search = search;
            var Users = Userservice.Get();
            var items = Teamservice.GetHoursUsers(search);
            ViewBag.AlertMore180Hours = Teamservice.GetHoursMore180Dashboard(items);
            ViewBag.Dates = Teamservice.DatesBySearch(search.FromDate, search.ToDate);
            ViewBag.Users = Users;
            return View(items);
        }

        [Route("dashboard")]
        public ActionResult Dashboard()
        {
            var items = Projectservice.Get();
            var dashboardProjects = new List<DashboardViewModel>();
            foreach (var item in items)
            {

                var Projects = new DashboardViewModel();
                Projects.ProjectId = item.Id;
                var Outcomes = Outcomeservice.Get(item.Id);
                Projects.OutcomesTotal =Convert.ToInt32(item.TaskTotalCost);
            //    var closestRisks = Riskservice.GetNextRisks(item.Id);
            //    var days = Enumerable.Range(0, 1 + (closestRisks.Date - DateTime.Now).Days)
            //    .Select(offset => (DateTime.Now).AddDays(offset)).Count();
            //    Projects.NextRisks = days;
             //   Projects.Priority = closestRisks.PriorityName;
              //  Projects.OwnerRisks = closestRisks.UsersName;
                Projects.ProjectsName = item.Name;
              //  var jira = JiraService.GetJiraData(item.Id);
               // Projects.OpenIsuues = jira.OpenIssues;
               // Projects.CloseIssues = jira.CloseIssues;
               // Projects.AllIssues = jira.AllIssues;
                Projects.OutcomesUntilNow = Convert.ToInt32(Outcomes.Sum(i => i.Cost));
                Projects.OuotcomeDiff = item.TaskTotalCost - Projects.OutcomesUntilNow;
                //Projects.OpenSprint = jira.OpenSprint;
               // Projects.CountSprint = jira.CountSprint;
               // Projects.ClosestSprint = jira.ClosestSprint;
                //Projects.DateActiveSprint = jira.DateActiveSprint;
               //Projects.AllIssuesInActiveSprint = jira.AllIssuesInActiveSprint;
               // Projects.ClosedIssuesInActiveSprint = jira.ClosedIssuesInActiveSprint;
              //  Projects.OpenIssuesInActiveSprint = jira.OpenIssuesInActiveSprint;
              //  var percentage = Projects.OuotcomeDiff * 100 / Projects.OutcomesTotal ;
            //    Projects.DiffBudgetPercentage = Projects.OuotcomeDiff * 100 / item.TaskTotalCost;

                //    percentage = Math.Floor(percentage);
             //   Projects.PercentageBudget = percentage;
              
          
             //  Projects.ClosestIssuesPercentage= jira.CloseIssues*100/ jira.AllIssues;
                dashboardProjects.Add(Projects);
            }
          //  ViewBag.Projects = Projectservice.Get();
            return View(dashboardProjects);
        }

        [Route("timesheet")]
        public ActionResult Timesheet()
        {

            return View();
        }

        [Route("budget")]
        public ActionResult Budget(BudgetSearchViewModel search)
        {
            var Projects = Projectservice.Get();
            Projects.Insert(0, new ProjectsViewModel() { Id = 0, Name = "All" });
            ViewBag.Projects = Projects;
            var inc = new BudgetSearchViewModel();
            var outc = new OutcomesearchViewModel();
            var incomes = Incomeservice.Get(search);
            var Outcomes = Outcomeservice.Get(search);
            ViewBag.Search = search;
            var items = new BudgetViewModel()
            {
                Incomes = incomes,
                Outcomes = Outcomes,
                IncomesTotal = incomes.Sum(i => i.Cost),
                OutcomesTotal = Outcomes.Sum(i => i.Cost),
            };
            return View(items);
        }


        [Route("statisticts")]
        public ActionResult Statisticts()
        {
            
            var Users = Userservice.Get().ToList();
            List<int> Hours = new List<int>();
            List<int> Status = new List<int>();
            List<int> incomes = new List<int>();
            List<int> outcomes = new List<int>();
            Status.Add(Taskservice.GetNumOfProjectStatuses("DONE"));
            Status.Add(Taskservice.GetNumOfProjectStatuses("in progress"));
            Status.Add(Taskservice.GetNumOfProjectStatuses("new"));
            ViewBag.Status = Status;
            for (int i = 0; i < 12; i++)
            {
                DateTime date = new DateTime(DateTime.Today.Year, i + 1, 1);
                Hours.Add(Teamservice.GetHoursForMonth(date));
                incomes.Add(Incomeservice.GetMounthlyIncomesSum(date));
                outcomes.Add(Outcomeservice.GetMounthlyOutcomesSum(date));
            }
            ViewBag.Hours = Hours;
            ViewBag.Incomes = incomes;
            ViewBag.Outcomes = outcomes;

            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        //[Route("CreateProjects")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateProjects([Bind(PreFixes = "ProjectsViewModel")]ProjectsViewModel model)
        //{
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //  //  Projectservice.Add(model);
        //    return Redirect("~/Projects");
        //}

        #region Projects
        [Route("Projects/new")]
        [HttpGet]
        public ActionResult ProjectNew()
        {
            ViewBag.Users = Userservice.Get();
            ViewBag.Status = ProjectStatusesService.Get();

            return View("ProjectEdit", new ProjectsViewModel() { StartDate = DateTime.Now, EndDate = DateTime.Now, AccountId = 1 });
        }

        [Route("Projects/{id}")]
        [HttpGet]
        public ActionResult ProjectEdit(int id)
        {
            ViewBag.Status = ProjectStatusesService.Get();
            var item = Projectservice.Single(id);
            ViewBag.Users = Userservice.Get();
            return View("ProjectEdit", item);
        }

        [Route("ProjectsShow/{id}")]
        [HttpGet]
        public ActionResult ProjectShow(int id)
        {
            ViewBag.Status = ProjectStatusesService.Get();
            var item = Projectservice.Single(id);
            ViewBag.Users = Userservice.Get();
            return View(item);
        }

        [Route("Projectsave")]
        [HttpPost]
        public ActionResult Projectsave(ProjectsViewModel model)
        {
            if (ModelState.IsValid == false)
            {

                var Users = Userservice.Get();
                ViewBag.Status = ProjectStatusesService.Get();
                ViewBag.Users = new SelectList(Users, "Id", "Name");
                return View(model);
            }

            Projectservice.Save(model);
            RolesService.New(Projectservice.GetLast(), "group mannager");

            Userservice.Save(new RefUserProViewModel {UserId= ((UsersViewModel)Session["CurrentUsers"]).Id,IsOwner=true,ProjectId= Projectservice.GetLast(), ProjecrId= Projectservice.GetLast(), Task = new Services.Entities.TasksRoles { RoleId = RolesService.GetLast() } });
            return View("CloseAndRefresh");
        }

        [Route("Projects/{id}/delete")]
        [HttpGet]
        public ActionResult DeleteProjects(int id)
        {
            Projectservice.Delete(id);
            return Redirect("~/Projects");
        }
        #endregion
    }
}