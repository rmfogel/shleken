using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class ProjectsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        [Required]
        public int AccountId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime EndDate { get; set; }

        [Display(Name="Status")]
        public string StatusName { get; set; }

        [Required]
        public int Status { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "Jira Url")]
        [Required]
        public string JiraUrl { get; set; }

        [Display(Name = "Jira Users Name")]
        [Required]
        public string JiraUserName { get; set; }
        [Display(Name = "Jira Password")]
        [Required]
        public string JiraPassword { get; set; }
        [Display(Name = "Jira Projects Key")]

        public string JiraProjectkey { get; set; }

        public int TaskTotalCost { get; set; }
        public int TaskTotalHours { get; set; }
        public int TeamTotalCost { get; set; }
        public int TeamTotalHours { get; set; }

      

        
    }

   
}
