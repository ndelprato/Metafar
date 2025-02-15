using Metafar.Core.Models;

namespace Metafar.Infraestructura;

public interface ISaldoRepositorio
{
    Saldo GetSaldo(string tarjeta);

    bool ActualizarSaldo(Saldo saldo);

    bool ExisteSaldo(string tarjeta);
    bool Guardar();

}
