using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class ContactsViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
