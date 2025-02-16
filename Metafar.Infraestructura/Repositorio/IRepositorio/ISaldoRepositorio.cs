using Metafar.Core.Models;

namespace Metafar.Infraestructura;

public interface ISaldoRepositorio
{
    Saldo GetSaldo(string tarjeta);

    bool ActualizarSaldo(Saldo saldo);

    bool ActualizarMonto(Saldo saldo, string tarjeta);

    bool ExisteSaldo(string tarjeta);
    bool Guardar();

}
