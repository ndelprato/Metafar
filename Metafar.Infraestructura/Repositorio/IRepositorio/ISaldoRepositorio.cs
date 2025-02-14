using Metafar.Core.Models;

namespace Metafar.Infraestructura;

public interface ISaldoRepositorio
{
    Saldo getSaldo(int tarjeta);

    bool actualizarSaldo(Saldo saldo);

}
