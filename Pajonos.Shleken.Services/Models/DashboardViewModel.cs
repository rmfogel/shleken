using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public class DashboardViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectsName { get; set; }
        public int NextRisks { get; set; }
        public string Priority { get; set; }
        public string OwnerRisks { get; set; }
        public int OpenIsuues { get; set; }
        public int CloseIssues { get; set; }
        public int AllIssues { get; set; }
        public float ClosestIssuesPercentage { get; set; }
        public float ClosestSprintPercentage { get; set; }
        public float PercentageBudget { get; set; }
        public int OutcomesUntilNow { get; set; }
        public int OutcomesTotal { get; set; }
        public int OuotcomeDiff { get; set; }
        public int DiffBudgetPercentage { get; set; }
        public int DateActiveSprint { get; set; }
        public float CountSprint { get; set; }
        public float ClosestSprint { get; set; }
        public float OpenSprint { get; set; }
        public float ClosedIssuesInActiveSprint { get; set; }
        public float OpenIssuesInActiveSprint { get; set; }
        public float AllIssuesInActiveSprint { get; set; }

    }
}
