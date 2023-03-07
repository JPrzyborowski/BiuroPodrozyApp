using System.ComponentModel.DataAnnotations;

namespace BiuroPodrozyApp.Models
{
    public class UserWycieczka
    {
        public int Id { get; set; }
        [Display(Name = "Identyfikator wycieczki")]
        public int WycieczkaId { get; set; }
        [Display(Name = "Identyfikator klienta")]
        public string UserId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public Wycieczka? Wycieczka { get; set; }
    }
}
