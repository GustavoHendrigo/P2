using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public interface IEstrategiaDeDesconto
    {
        decimal CalcularDesconto(Pedido pedido);
    }
}
