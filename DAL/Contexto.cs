using Microsoft.EntityFrameworkCore;
using TicketsApp.Models;

namespace TicketsApp.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Sistemas> Sistemas { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

    }
}
