using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class RepositorioDePedidos : IRepositorioDePedidos
    {
        private readonly List<Pedido> _pedidos;

        public RepositorioDePedidos()
        {
            _pedidos = new List<Pedido>();
        }

        public void Add(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public Pedido GetById(int id)
        {
            return _pedidos.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Pedido> GetAll()
        {
            return _pedidos.AsReadOnly();
        }
    }
}
