using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pajonos.Shleken.Services.Models
{
   public class MeetingsViewModel
    {
        [Key]
        public int Id { get; set; }

         [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Members { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Agenda { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Actions { get; set; }

        [Required]
        public string Location { get; set; }
     
        public int UserId { get; set; }
    
        public int ProjectId { get; set; }

        public override string ToString()
        {
            return $"agenda: {Agenda} location: {Location} date: {Date.ToShortDateString()} " ;
        }

    }
}
