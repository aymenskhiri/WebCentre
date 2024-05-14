using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebCentre.Models;

namespace WebCentre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReservationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reservations/Salles
        [HttpGet("Salles")]
        public async Task<ActionResult<IEnumerable<Salle>>> GetAvailableSalles()
        {
            var availableSalles = await _context.Salles.ToListAsync();
            return Ok(availableSalles);
        }

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            // Check if the salle is available for the specified date and time
            var salle = await _context.Salles.FindAsync(reservation.IdSalle);
            if (salle == null)
            {
                return NotFound("Salle not found");
            }

            // Perform additional checks to ensure the salle is available for the reservation

            // Assuming validation passes, create the reservation
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }
        // GET: api/reservations/GetReservations
        [HttpGet("GetReservations")]
        public async Task<ActionResult<IEnumerable<object>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.IdProfNavigation) // Include the Professeur navigation property
                .ToListAsync();

            var reservationsWithDetails = reservations.Select(r => new
            {
                r.Id,
                r.DateRes,
                r.HeureDebut,
                r.HeureFin,
                r.Status,
                ProfesseurNom = GetUserNom(r.IdProfNavigation.UserId), // Get Nom of associated User
                ProfesseurPrenom = GetUserPrenom(r.IdProfNavigation.UserId) // Get Prenom of associated User
            });

            return Ok(reservationsWithDetails);
        }

        // Helper method to retrieve Nom of User based on UserId
        private string GetUserNom(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user?.Nom ?? "Unknown";
        }

        // Helper method to retrieve Prenom of User based on UserId
        private string GetUserPrenom(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user?.Prenom ?? "Unknown";
        }

        [HttpGet("GetReservationsByProf/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByProf(int id)
        {

            var r = await _context.Reservations.Where(r => r.IdProf == id).ToListAsync();
            return Ok(r);
        }


        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }
    }
}
