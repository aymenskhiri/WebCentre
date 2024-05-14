namespace WebCentre.Models
{
    public partial class Salle
    {
        public int Id { get; set; }

        public int NumSalle { get; set; }

        public int? Capacité { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}