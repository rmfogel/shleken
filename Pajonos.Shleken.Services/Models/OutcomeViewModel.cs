using Pajonos.Shleken.Services.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public class OutcomesViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Desination { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }

        [Display(Name ="Approver Name")]
        [Required]
        public int ApproverId { get; set; }

        [Display(Name = "Approver Name")]
        public string ApproverName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime Date { get; set; }

        [Required]
        public double Cost { get; set; }

        public int ProjectId { get; set; }

        public virtual Projects Projects { get; set; }

        public List<RefUserPro> Users { get; set; }
    }
}
