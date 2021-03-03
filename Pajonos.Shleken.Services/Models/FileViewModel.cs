using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public class FilesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

    }
}
