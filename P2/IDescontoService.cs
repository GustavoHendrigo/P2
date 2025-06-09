using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    // Interface para o serviço de aplicação de descontos
    public interface IDescontoService
    {
        decimal AplicarDesconto(Pedido pedido, IEstrategiaDeDesconto estrategia);
    }
}
