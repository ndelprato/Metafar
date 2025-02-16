using Metafar.Core.Models;
using Metafar.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Infraestructura;

public class OperacionesRepositorio : IOperacionesRepositorio
{
    private readonly ApplicationDbContext _bd;
    public OperacionesRepositorio(ApplicationDbContext bd)
    {
        _bd = bd;
    }
    /*public ICollection<Operaciones> GetOperacionesTarjeta(string tarjeta)
    {
        return _bd.Operaciones.Where(o => o.Tarjeta == tarjeta).ToList();
    }
*/
    public ICollection<Operaciones> GetOperacionesTarjetaPaginado(string tarjeta, int pageNumber, int pageSize)
    {
        return _bd.Operaciones.Where(o => o.Tarjeta == tarjeta)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();        
    }

    public int GetTotalOperaciones()
    {
        return _bd.Operaciones.Count();
    }

    public bool GuardarOperacion(Operaciones operacion)
    {
        operacion.Fecha = DateTime.Now;
        _bd.Operaciones.Add(operacion);
        return Guardar();
    }

    public bool Guardar()
    {
        return _bd.SaveChanges() >= 0 ? true : false;
    }

  

    public ICollection<Operaciones> GetOperaciones()
    {
       return _bd.Operaciones.OrderBy(c => c.Tarjeta).ToList();
    }
}
