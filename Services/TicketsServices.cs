using TicketsApp.DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Models;

using System.Linq.Expressions;


namespace TicketsApp.Services
{
    public class TicketsServices
    {
        private readonly Contexto _contexto;

        public TicketsServices(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int ticketId)
        {
            return await _contexto.Tickets
                .AnyAsync(t => t.TicketId == ticketId);
        }

        private async Task<bool> Insertar(Tickets ticket)
        {
            _contexto.Tickets.Add(ticket);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tickets ticket)
        {
            var entry = _contexto.Entry(ticket);

            if (entry.State == EntityState.Detached)
            {
                _contexto.Update(ticket);
            }
            else
            {
                entry.State = EntityState.Detached;
                _contexto.Attach(ticket);
                _contexto.Entry(ticket).State = EntityState.Modified;
            }

            int modificado = await _contexto.SaveChangesAsync();
            return modificado > 0;
        }

        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
                return await Insertar(ticket);
            else
                return await Modificar(ticket);
        }

        public async Task<bool> Eliminar(Tickets ticket)
        {
            try
            {
                _contexto.Entry(ticket).State = EntityState.Deleted;
                bool changes = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(ticket).State = EntityState.Detached;
                return changes;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tickets? BuscarPorDescripcion(string? descripcion)
        {
            return _contexto.Tickets.SingleOrDefault(t => t.Descripcion == descripcion);
        }

                public async Task<Tickets?> Buscar(int ticketId)
        {
            return await _contexto.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
        {
            return await _contexto.Tickets
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
