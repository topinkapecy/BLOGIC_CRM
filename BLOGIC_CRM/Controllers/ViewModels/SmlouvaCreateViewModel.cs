using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BLOGIC_CRM.ViewModels

{
    public class SmlouvaCreateViewModel
    {
        [Required]
        [Display(Name = "Evidenční číslo")]
        public string EvidencniCislo { get; set; } = string.Empty;

        [Required]
        public string Instituce { get; set; } = string.Empty;

        [Required(ErrorMessage = "Klient je povinný.")]
        [Display(Name = "Klient")]
        public int KlientId { get; set; }

        [Required(ErrorMessage = "Správce je povinný.")]
        [Display(Name = "Správce smlouvy")]
        public int SpravceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Datum uzavření")]
        public DateTime DatumUzavreni { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Datum platnosti")]
        public DateTime DatumPlatnosti { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        [Display(Name = "Datum ukončení")]
        public DateTime? DatumUkonceni { get; set; } //smlouva nemusí mít konec


        [Required(ErrorMessage = "Musí být vybrán alespoň jeden účastník.")]
        [Display(Name = "Účastníci (Poradci)")]
        public List<int> VybraniUcastniciIds { get; set; } = new List<int>(); // M:N vztah, víc poradců najednou

        //zobrazíme seznamy jmen klientu a poradců pomocí VIEW
        //ukládáme poté jen IDs klientů a poradců, ne jména
        public IEnumerable<SelectListItem>? KlientiList { get; set; }
        public IEnumerable<SelectListItem>? PoradciList { get; set; }
    }
}                   
