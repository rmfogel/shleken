using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class ProjectsSearchViewModel
    {
        public string Name { get; set; }
        [Display(Name = "Owner")]
        public int OwnerId { get; set; }
        public int Status { get; set; }
    }
}
