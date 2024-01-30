using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TicketsApp.Models { 
    public class Clientes{
    
    [Key]

    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string Nombres { get; set; } = string.Empty;
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string ?  Telefono{ get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
    public string ? Celular{ get; set; }
    [RegularExpression(@"^\d{3}-\d{7}-\d{1}$", ErrorMessage = "Formato de RNC no válido")]
    public string ? Rnc{ get; set; }
    [EmailAddress(ErrorMessage = "Formato de correo electrónico no válido")]
    public string ? Email{ get; set; }
    [Required(ErrorMessage = "Campo Obligatorio")]
       
    public string ? Direccion{ get; set; }


    }
    }