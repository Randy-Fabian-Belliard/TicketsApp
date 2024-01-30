using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TicketsApp.Models {     
    public class Sistemas{
        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "El nombre del sistema es obligatorio")]
        public string? Nombre { get; set; }

        [ForeignKey("Sistemas")]
         public ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();


       
    }

}