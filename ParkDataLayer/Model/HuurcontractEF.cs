using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    [Table("Huurcontract")]
    public class HuurcontractEF
    {
        [Key]
        [MaxLength(25)]
        public string Id { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [Required]
        public int AantalDagen{ get; set; }
        public int HuurderId { get; set; }
        public HuurderEF Huurder { get; set; }
        public int HuisId { get; set; } 
        public HuisEF Huis { get; set; }

    }
}
