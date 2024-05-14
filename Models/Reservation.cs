using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebCentre.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }

        public string? DateRes { get; set; }

        public string? HeureDebut { get; set; }

        public string? HeureFin { get; set; }

        public string? Status { get; set; }

        public int IdProf { get; set; }

        public int IdSalle { get; set; }

        public virtual Professeur IdProfNavigation { get; set; } = null!;

        public virtual Salle IdSalleNavigation { get; set; } = null!;
    }
}