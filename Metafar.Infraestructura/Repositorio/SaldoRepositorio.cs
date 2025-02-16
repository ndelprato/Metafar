using Metafar.Core.Models;
using Metafar.Infraestructura.Data;

namespace Metafar.Infraestructura;

public class SaldoRepositorio : ISaldoRepositorio
{
    private readonly ApplicationDbContext _bd;

    public SaldoRepositorio(ApplicationDbContext bd)
    {
        _bd = bd;
    }
    public bool ActualizarSaldo(Saldo saldo)
    {
        saldo.Fecha = DateTime.Now;
        
        _bd.Saldo.Update(saldo);
        return Guardar();
    }

    public bool ActualizarMonto(Saldo saldo, string tarjeta)
    {
        saldo.Fecha = DateTime.Now;
        //Arreglar problema del patch
        var saldoExistente = _bd.Saldo.Find(tarjeta);
        saldo.Id = saldoExistente.Id;
        saldo.Tarjeta = tarjeta;


        _bd.Saldo.Update(saldo);
        return Guardar();
    }

    public bool ExisteSaldo(string tarjeta)
    {
        return _bd.Saldo.Any(s => s.Tarjeta == tarjeta);
    }

   

    public Saldo GetSaldo(string tarjeta)
    {
        return _bd.Saldo.FirstOrDefault(s => s.Tarjeta == tarjeta);
    }

    public bool Guardar()
    {
        return _bd.SaveChanges() >= 0 ? true : false;
    }
}
