using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pajonos.Shleken.Services.Models
{
   public class FixesViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool ShowClient { get; set; }
        public int ProjectId { get; set; }

    }
}
