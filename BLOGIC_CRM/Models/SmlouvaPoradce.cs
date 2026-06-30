namespace BLOGIC_CRM.Models
{
    public class SmlouvaPoradce
    {
        public int SmlouvaId { get; set; }
        public Smlouva Smlouva { get; set; } = null!;

        public int PoradceId { get; set; }
        public Poradce Poradce { get; set; } = null!;
    }
}