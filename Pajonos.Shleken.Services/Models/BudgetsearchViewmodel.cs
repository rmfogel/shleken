using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pajonos.Shleken.Services.Models
{
    public  class BudgetSearchViewModel
    {
        public string Description { get; set; }

        [Display(Name = "Approver Name")]
        public int ApproverId { get; set; }

        [Display(Name = "Projects Name")]
        public int ProjectId { get; set; }

        [Display(Name = "From Date")]
        public System.DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        public System.DateTime? ToDate { get; set; }

        public double Cost { get; set; }
    }
}
