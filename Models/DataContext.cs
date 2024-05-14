using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WebCentre.Models
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {
         }
        public virtual DbSet<Professeur> Professeurs { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Salle> Salles { get; set; }


        


    }
}
    

