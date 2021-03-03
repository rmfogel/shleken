using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services
{
   public static class ToDoListStatusesService
    {
        public static List<ToDoListStatusesViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.ToDoListStatuses
                    .Map<ToDoListStatuses, ToDoListStatusesViewModel>();
            }
        }
    }
}
