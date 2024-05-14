namespace WebCentre.Models
{
    public partial class Professeur
    {
        public int Id { get; set; }

        public string? Specialite { get; set; }

        public string? Matiere { get; set; }

        public string UserId { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


    }
}