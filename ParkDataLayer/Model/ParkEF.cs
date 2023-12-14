using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkEF
    {
        [Key]
        [MaxLength(255)]
        public string id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
    }
}
