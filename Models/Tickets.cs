using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TicketsApp.Models {     
    public class Tickets{
        [Key]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Este campo es necesario")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int PrioridadId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string? SolicitadoPor { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string? Asunto { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string? Descripcion { get; set; }

    }
}