using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class ToDoListsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        [Display(Name = "Status")]
        public int IsDone { get; set; }

        [Display(Name ="Status")]
        public string Status { get; set; }

        public System.DateTime CreatedDate { get; set; }


        public int UserId { get; set; }

        public string UsersName { get; set; }

        public int ProjectId { get; set; }

 
    }
}
