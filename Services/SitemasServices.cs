using TicketsApp.DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Models;
using System.Linq.Expressions;


namespace TicketsApp.Services
{
    public class SistemasServices
    {
        private readonly Contexto _contexto;

        public SistemasServices(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int sistemaId)
        {
            return await _contexto.Sistemas
                .AnyAsync(s => s.SistemaId == sistemaId);
        }

        private async Task<bool> Insertar(Sistemas sistema)
        {
            _contexto.Sistemas.Add(sistema);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Sistemas sistema)
        {
            var entry = _contexto.Entry(sistema);

            if (entry.State == EntityState.Detached)
            {
                _contexto.Update(sistema);
            }
            else
            {
                entry.State = EntityState.Detached;
                _contexto.Attach(sistema);
                _contexto.Entry(sistema).State = EntityState.Modified;
            }

            int modificado = await _contexto.SaveChangesAsync();
            return modificado > 0;
        }

        public async Task<bool> Guardar(Sistemas sistema)
        {
            if (!await Existe(sistema.SistemaId))
                return await Insertar(sistema);
            else
                return await Modificar(sistema);
        }

        public async Task<bool> Eliminar(Sistemas sistema)
        {
            try
            {
                _contexto.Entry(sistema).State = EntityState.Deleted;
                bool changes = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(sistema).State = EntityState.Detached;
                return changes;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Sistemas? BuscarPorNombre(string? nombre)
        {
            return _contexto.Sistemas.SingleOrDefault(s => s.Nombre == nombre);
        }

        public async Task<Sistemas?> Buscar(int sistemaId)
        {
            return await _contexto.Sistemas
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
        }

        public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
        {
            return await _contexto.Sistemas
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
