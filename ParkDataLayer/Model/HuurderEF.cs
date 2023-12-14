using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    [Table("Huurder")]
    public class HuurderEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  
        [Required]
        [MaxLength(50)]  
        public string Naam { get; set; }
        [MaxLength(100)]
        public string Telefoon { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Adres { get; set; }
        public List<HuurcontractEF> Huurcontracten { get; set; }

    }
}
