using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    [Table("Park")]
    public class ParkEF
    {
        [Key]
        [MaxLength(20)]
        public string Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Naam { get; set; }
        [MaxLength(500)]
        public string Location { get; set; }
        public List<HuisEF> Huizen { get; set; }   
    }
}
