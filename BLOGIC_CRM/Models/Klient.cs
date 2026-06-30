using System.ComponentModel.DataAnnotations;

namespace BLOGIC_CRM.Models
{
    public class Klient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Jméno klienta je povinné.")]
        [Display(Name = "Jméno")]
        public string Jmeno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení klienta je povinné.")] 
        [Display(Name = "Příjmení")]
        public string Prijmeni { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail klienta je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát e-mailu.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon klienta je povinný.")]
        [Phone(ErrorMessage = "Neplatný formát telefonního čísla.")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rodné číslo klienta je povinné.")]
        [StringLength(11)]
        [Display(Name = "Rodné číslo")]
        public string RodneCislo { get; set; } = string.Empty;

        [Range (0, 120)]
        [Required(ErrorMessage = "Věk klienta je povinný.")]
        [Display(Name = "Věk")]
        public int Vek { get; set; }    

        public ICollection<Smlouva> Smlouvy { get; set; } = new List<Smlouva>();

        [Display(Name = "Celé jméno")]
        public string CeleJmeno => $"{Jmeno} {Prijmeni}";

    }
}
