using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    
namespace Pajonos.Shleken.Services.Models
{
   public  class RisksearchViewModel
    {

        public string Name { get; set; }

        [Display(Name="Owner")]
        public int UserId { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public int Probabilty { get; set; }
    }
}
