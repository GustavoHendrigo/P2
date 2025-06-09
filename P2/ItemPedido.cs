using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class ItemPedido
    {
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal PrecoTotal => Quantidade * PrecoUnitario;

        public ItemPedido(Produto produto, int quantidade)
        {
            if (produto == null) throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
            if (quantidade <= 0) throw new ArgumentException("Quantidade tem que ser maior que 0.", nameof(quantidade));

            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;
        }
    }
}
