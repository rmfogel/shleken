using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;


namespace Pajonos.Shleken.Services
{
    public static class RiskStatusesService
    {
        
            public static List<RiskStatusesViewModel> Get()
            {
                using (var db = new ShlekenEntities3())
                {
                    return db.RiskStatuses
                        .Map<RiskStatuses, RiskStatusesViewModel>();
                }
            }
      
    }
}
