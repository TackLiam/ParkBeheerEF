using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    [Table("Huis")]
    public class HuisEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public string Straat { get; set; }
        [Required]
        public int Nr { get; set; }
        [Required]
        public bool Actief { get; set; }


        public string ParkId { get; set; }
        public ParkEF Park { get; set; }
        public List<HuurcontractEF> Huurcontracten { get; set; }

    }
}
