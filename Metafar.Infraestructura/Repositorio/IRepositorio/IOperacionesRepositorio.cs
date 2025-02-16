using Metafar.Core.Models;

namespace Metafar.Infraestructura;

public interface IOperacionesRepositorio
{
     //ICollection<Operaciones> GetOperacionesTarjeta(string tarjeta);
     ICollection<Operaciones> GetOperacionesTarjetaPaginado(string tarjeta, int pageNumber, int pageSize);
     int GetTotalOperaciones();

     ICollection<Operaciones> GetOperaciones();
    bool GuardarOperacion(Operaciones operacion);

    bool Guardar();
}
