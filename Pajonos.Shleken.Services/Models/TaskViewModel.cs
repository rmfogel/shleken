using Pajonos.Shleken.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public bool ShowClient { get; set; }
        public string Status { get; set; }

        public List<TasksRoles> AllTasksRoles { get; set; }
        public virtual ICollection<TasksRoles> TasksRoles { get; set; }

      
    }
}
