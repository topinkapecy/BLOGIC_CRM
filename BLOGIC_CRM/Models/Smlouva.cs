using System.ComponentModel.DataAnnotations;

namespace BLOGIC_CRM.Models
{
    public class Smlouva
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Evidenční číslo je povinné.")]
        [Display(Name = "Evidenční číslo")]
        public string EvidencniCislo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Klient je povinný.")]
        public int KlientId { get; set; }
        public Klient Klient { get; set; } = null!;

        [Required(ErrorMessage = "Správce smlouvy je povinný.")]
        [Display(Name = "Správce smlouvy")]
        public int SpravceSmlouvyId { get; set; }
        public Poradce SpravceSmlouvy { get; set; } = null!;

        [Required(ErrorMessage = "Datum uzavření je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum uzavření")]
        public DateTime DatumUzavreni { get; set; }

        [Required(ErrorMessage = "Datum platnosti je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum platnosti")]
        public DateTime DatumPlatnosti { get; set; }

        [Required(ErrorMessage = "Datum ukončení je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum ukončení")]
        public DateTime DatumUkonceni { get; set; }

        public ICollection<SmlouvaPoradce> SmlouvaPoradce { get; set; } = new List<SmlouvaPoradce>();
    }
}
