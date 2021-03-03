using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class JiraViewModel
    {
        public int OpenIssues { get; set; }
        public int AllIssues { get; set; }
        public int CloseIssues { get; set; }
        public int DateActiveSprint { get; set; }
        public float CountSprint { get; set; }
        public float ClosestSprint { get; set; }
        public float OpenSprint { get; set; }
        public float ClosestSprintPercentage { get; set; }
        public float ClosedIssuesInActiveSprint { get; set; }
        public float OpenIssuesInActiveSprint { get; set; }
        public float AllIssuesInActiveSprint { get; set; }

    }
    public class Issues
    {
        public string expand { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<Status> issues { get; set; }
    }

    public class Status
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public StatusField status { get; set; }
    }

    public class StatusField
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory statusCategory { get; set; }

    }
    public class StatusCategory
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }


    public class Sprints
    {
        public int maxResults { get; set; }
        public int startAt { get; set; }
        public bool isLast { get; set; }
        public List<Sprint> values { get; set; }
    }
    public class Sprint
    {
        public int id { get; set; }
        public string self { get; set; }
        public string state { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string completeDate { get; set; }
        public int originBoardId { get; set; }
        public string goal { get; set; }
    }
    public class Boards
    {
        public int maxResults { get; set; }
        public int startAt { get; set; }
        public int total { get; set; }
        public bool isLast { get; set; }
        public List<Board> values { get; set; }
    }


    public class Board
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("self")]
        public string self { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("location")]
        public Location location { get; set; }

    }

    public class Location
    {
        public int ProjectId { get; set; }
        public string name { get; set; }
        public string ProjectsTypeKey { get; set; }
        public string avatarURI { get; set; }
    }
}
