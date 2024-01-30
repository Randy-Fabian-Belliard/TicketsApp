using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketsApp.Models;
using TicketsApp.DAL;

public class ClientesServices
{
    private Contexto _contexto;

    public ClientesServices(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.clientes.AnyAsync(p => p.ClienteId == id);
    }

    private async Task<bool> Insertar(Clientes clientes)
    {
        await _contexto.clientes.AddAsync(clientes);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes clientes)
    {
        _contexto.Entry(clientes).State = EntityState.Modified;
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Eliminar(Clientes cliente)
    {
        try
        {
            _contexto.Entry(cliente).State = EntityState.Deleted;
            bool changes = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(cliente).State = EntityState.Detached;
            return changes;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Clientes?> Buscar(int ClienteId)
    {
        return await _contexto.clientes
            .Where(o => o.ClienteId == ClienteId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _contexto.clientes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
        return await _contexto.clientes.AnyAsync(p => p.Nombres == nombre);
    }

    public async Task<bool> ExisteRnc(string rnc)
    {
        return await _contexto.clientes.AnyAsync(p => p.Rnc == rnc);
    }
}
