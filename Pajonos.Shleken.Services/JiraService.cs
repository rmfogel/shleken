using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Atlassian.Jira;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
    public static class JiraService
    {
        public static  JiraViewModel GetJira(int ProjectsId)
        {
            using (var db = new ShlekenEntities3())
            {
                var modelJira = new JiraViewModel();
                var Projects = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectsId);
                if (Projects.JiraUrl != null)
                {
                    var jira = Jira.CreateRestClient(Projects.JiraUrl, Projects.JiraUserName, Projects.JiraPassword);
              
                   
                var issues = from i in jira.Issues.Queryable
                             where i.Project == Projects.JiraProjectkey
                             select i;
                }
                //todo check out
                //modelJira.OpenIssues =  issues.Where(i => i.Status != "Closed").Count();
                //modelJira.CloseIssues =  issues.Where(i => i.Status == "Closed").Count();
                //modelJira.AllIssues = issues.Count();
                return modelJira;
            }
        }

        //public static JiraViewModel GetJiraData(int ProjectsId)
        //{
        //      try { 
        //      using (var db = new ShlekenEntities3())
        //      {
        //          var Projects = db.Projects.Single(i => i.AccountId == Userservice.AccountId && i.Id == ProjectsId);
        //          string boards = RunQuery("agile/1.0/board", Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //          var resultBoard = JsonConvert.DeserializeObject<Boards>(boards);
        //          //var ProjectsName = "MSP";
        //          List<int> boardsId = new List<int>();
        //          foreach (var board in resultBoard.values)
        //          {
        //              if (board.location.name.Contains(Projects.JiraProjectkey))
        //              {
        //                  boardsId.Add(board.id);
        //              }
        //          }
        //          //List<Issues> issues = new List<Issues>();
        //          //foreach(var id in boardsId)
        //          //{
        //          //    var issue= RunQuery("board/" + id + "/issue", Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //          //    var result = JsonConvert.DeserializeObject<Issues>(issue);
        //          //    issues.Add(result);
        //          //}

        //          var closedIssuesInActiveSprint=new Issues();
        //          var allIssuesInActiveSprint= new Issues();
        //          List<Issues> listIssues = new List<Issues>();
        //          List<Sprints> sprints = new List<Sprints>();
        //          foreach (var id in boardsId)
        //          {
        //              var sprint = RunQuery("agile/1.0/board/" + id + "/sprint", Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //              var result = JsonConvert.DeserializeObject<Sprints>(sprint);
        //              sprints.Add(result);
        //          }
        //          var modelJira = new JiraViewModel();
        //          int countClosed = 0;
        //          int countOpen = 0;
        //          int countAll = 0;
        //foreach(var sprint in sprints)
        //          {
        //              foreach(var s in sprint.values)
        //              {
        //                  countAll++;
        //                  if (s.state == "closed")
        //                  {
        //                      countClosed++;
        //                  }
        //                  if (s.state == "active" || s.state == "future")
        //                  {
        //                      countOpen++;
        //                  }
        //                  if (s.state == "active")
        //                  {
        //                      var issueClose = RunQuery("api/2/search?jql=Sprint="+ s.id+" AND Status='DONE'&fields=status", Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //                       closedIssuesInActiveSprint = JsonConvert.DeserializeObject<Issues>(issueClose);
        //                      var issueAll= RunQuery("api/2/search?jql=Sprint=" + s.id + "&fields=status", Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //                       allIssuesInActiveSprint = JsonConvert.DeserializeObject<Issues>(issueAll);
        //                      String value = s.endDate;
        //                      Char delimiter = 'T';
        //                      String[] substrings = value.Split(delimiter);
        //                      DateTime date =DateTime.Parse(substrings[0]);
        //                    var t= (date - DateTime.Now).Days;
        //                      modelJira.DateActiveSprint = t-1;
        //                  }
        //              }
        //          }
        //          modelJira.AllIssuesInActiveSprint = allIssuesInActiveSprint.issues.Count();
        //               modelJira.ClosedIssuesInActiveSprint = closedIssuesInActiveSprint.issues.Count();
        //          modelJira.OpenIssuesInActiveSprint = modelJira.AllIssuesInActiveSprint - modelJira.ClosedIssuesInActiveSprint;
        //          modelJira.ClosestSprint = countClosed;
        //          modelJira.CountSprint = countAll;
        //          modelJira.OpenSprint = countOpen;
        //          modelJira.ClosestSprintPercentage = countClosed / countAll * 100;
        //          //var jira = Jira.CreateRestClient(Projects.JirUrl, Projects.JiraUserName, Projects.JiraPassword);
        //          //var issues = from i in jira.Issues.Queryable
        //          //             where i.Projects == Projects.JiraProjectkey
        //          //             select i;
        //          //modelJira.OpenIssues = issues.Where(i => i.Status != "Closed").Count();
        //          //modelJira.CloseIssues = issues.Where(i => i.Status == "Closed").Count();
        //          //modelJira.AllIssues = issues.Count();
        //          return modelJira;
        //      }
        //      }
        //      catch
        //      {
        //          var modal = new JiraViewModel();
        //          return modal;
        //      }

        //}


        public static string RunQuery(string jiraResource,string jiraUrl,string UsersName, string password, string argument = null, string data = null, string method = "GET")
        {
            string url = string.Format("{0}{1}", jiraUrl +"/rest/", jiraResource);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = method;

            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                }
            }

            string base64Credentials = GetEncodedCredentials(UsersName, password);
            request.Headers.Add("Authorization", "Basic " + base64Credentials);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static string GetEncodedCredentials(string UsersName,string password)
        {
            string mergedCredentials = string.Format("{0}:{1}", UsersName, password);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }


}



