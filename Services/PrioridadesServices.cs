using TicketsApp.DAL;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Models;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace TicketsApp.Services
{
    public class PrioridadesServices
    {
        private readonly Contexto _contexto;
        public PrioridadesServices(Contexto contexto)
        {
            _contexto = contexto;
        }
   public async Task<bool> Existe(int PrioridadId)
    {
        return await _contexto.Prioridades
            .AnyAsync(p => p.PrioridadId == PrioridadId);
    }
    private async Task<bool> Insertar(Prioridades prioridad)
    {
        _contexto.Prioridades.Add(prioridad);
        return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Modificar(Prioridades prioridades)
    {
        var entry = _contexto.Entry(prioridades);

        if (entry.State == EntityState.Detached)
        {
            _contexto.Update(prioridades);
        }
        else
        {
            entry.State = EntityState.Detached;
            _contexto.Attach(prioridades);
            _contexto.Entry(prioridades).State = EntityState.Modified;
        }

        int modificado = await _contexto.SaveChangesAsync();
        return modificado > 0;
    }


    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (! await Existe(prioridad.PrioridadId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);
    }

    public async Task<bool> Eliminar(Prioridades prioridades)
    {
        try
        {
            _contexto.Entry(prioridades).State = EntityState.Deleted;
            bool changes = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(prioridades).State = EntityState.Detached;
            return changes;
        }
        catch (Exception)
        {
            return false;
        }
    }

        public Prioridades? BuscarPorDescripcion(string? descripcion)
        {
            return _contexto.Prioridades.SingleOrDefault(p => p.Descripcion == descripcion);
        }
        public async Task<Prioridades?> Buscar(int prioridadId)
        {
            return await _contexto.Prioridades
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PrioridadId == prioridadId);
        }
    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
        {
            return await _contexto.Prioridades
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}