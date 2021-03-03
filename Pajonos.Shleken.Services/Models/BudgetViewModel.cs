using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pajonos.Shleken.Services.Entities;
using Pajonos.Shleken.Services.Models;

namespace Pajonos.Shleken.Services.Models
{
    public class BudgetViewModel
    {
        public List<IncomesViewModel> Incomes { get; set; }
        public List<OutcomesViewModel> Outcomes { get; set; }

        public double IncomesTotal { get; set; }
        public double OutcomesTotal { get; set; }
        public BudgetViewModel()
        {
            Incomes = new List<IncomesViewModel>();
            Outcomes = new List<OutcomesViewModel>();
        }
    }
    
}
