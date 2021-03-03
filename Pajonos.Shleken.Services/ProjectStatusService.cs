using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
   static public class ProjectStatusesService
    {
        public static List<ProjectStatusesViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.ProjectStatuses
                    .Map<ProjectStatuses, ProjectStatusesViewModel>();
            }
        }

      
    }
}
