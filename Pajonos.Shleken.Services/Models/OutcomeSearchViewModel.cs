using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pajonos.Shleken.Services.Models
{
  public  class OutcomesearchViewModel
    {
        public string Description { get; set; }

        [Display(Name = "Approver Name")]
        public int ApproverId { get; set; }

        public double Cost { get; set; }
    }
}
