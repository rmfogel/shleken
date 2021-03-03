using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pajonos.Shleken.Services.Models
{
    public class FilesearchViewModel
    {
        public string Name { get; set; }

        [Display(Name = "From Date")]
        public System.DateTime? FromDate { get; set; }


        [Display(Name = "To Date")]  
        public System.DateTime? ToDate { get; set; }
    }
}
