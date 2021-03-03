using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
    public class UsersSearchViewModel
    {
        [Display(Name = "users name")]

        public string Name { get; set; }

        [Display(Name = "users role")]

        public string Role { get; set; }
    }
}
