using System.ComponentModel.DataAnnotations;

namespace BiuroPodrozyApp.Models
{
    public class Wycieczka
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        [Display(Name ="Początek")]
        public DateTime DataOd { get; set; }
        [Display(Name = "Koniec")]
        public DateTime DataDo { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }

        public ICollection<UserWycieczka>? UserWycieczki { get; set; }
    }
}
