using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;


namespace Pajonos.Shleken.Services
{
  public static  class RiskProbabilitiesService
    {
        public static List<RiskProbabilitiesViewModel> Get()
        {
            using (var db = new ShlekenEntities3())
            {
                return db.RiskProbabilities
                    .Map<RiskProbabilities, RiskProbabilitiesViewModel>();
            }
        }
    }
}
