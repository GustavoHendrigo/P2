using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class PedidoFactory
    {
        private static int _idProximoPedido = 1; // Para gerar IDs dos pedidos

        public Pedido CriarPedido(Cliente cliente, IEnumerable<ItemPedido> items)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente), "Cliente não pode ser nulo.");
            if (items == null || !items.Any()) throw new ArgumentException("O pedido tem que ter pelo menos um item.", nameof(items));

            return new Pedido(_idProximoPedido++, cliente, DateTime.Now, items);
        }
    }
}
