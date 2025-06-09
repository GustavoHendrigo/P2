using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public interface IRepositorioDePedidos
    {
        void Add(Pedido pedido); // Adiciona um novo pedido
        Pedido GetById(int id); // Busca um pedido pelo ID
        IEnumerable<Pedido> GetAll(); // Retorna todos os pedidos
    }
}
