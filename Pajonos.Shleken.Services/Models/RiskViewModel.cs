using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public  class RisksViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Mitigations { get; set; }

        [Required]
        public int Priority { get; set; }

        [Display(Name = "Priority")]
        public string PriorityName { get; set; }

        [Required]
        public int Probabilty { get; set; }

        [Display(Name = "Probabilty")]
        public string ProbabiltyName { get; set; }

        [Required]
        public bool Budget { get; set; }

        [Required]
        public bool Delivery { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime Date { get; set; }

        [Display(Name="Users Name")]
        [Required]
        public int UserId { get; set; }

        [Display(Name="Owner")]
        public string UsersName { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int Status { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Required]
        public double Cost { get; set; }

        public IEnumerable<DropDownItem> MyList { get; set; }
    }
    
}
