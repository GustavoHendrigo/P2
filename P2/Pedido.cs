using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class Pedido
    {
        public int Id { get; private set; }
        public Cliente Cliente { get; private set; }
        public DateTime DataPedido { get; private set; }
        private List<ItemPedido> _items;
        public IReadOnlyList<ItemPedido> Items => _items.AsReadOnly();
        public decimal ValorTotal { get; private set; }

        public Pedido(int id, Cliente cliente, DateTime dataPedido, IEnumerable<ItemPedido> items)
        {
            if (id <= 0) throw new ArgumentException("Precisa ser maior que 0.", nameof(id));
            if (cliente == null) throw new ArgumentNullException(nameof(cliente), "Cliente não pode ser nulo.");
            if (items == null || !items.Any()) throw new ArgumentException("O pedido tem que ter pelo menos um item.", nameof(items));

            Id = id;
            Cliente = cliente;
            DataPedido = dataPedido;
            _items = new List<ItemPedido>(items);
            CalcularValorTotal();
        }

        // Método privado para calcular o valor total do pedido
        private void CalcularValorTotal()
        {
            ValorTotal = _items.Sum(item => item.PrecoTotal);
        }

        // O método AplicarDesconto será adicionado posteriormente, para aplicar o OCP.
    }
}
