using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pajonos.Shleken.Services.Models
{
    public class RolesResourcesViewModel
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }
       
        public int ProjectId { get; set; }
    }



   
}
