using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class ProdutoFactory
    {
        private static int _idProximoProduto = 1; // Para IDs dos produtos

        public Produto CriarProduto(string nome, decimal preco, string categoria)
        {
            // A validação já é feita no construtor de Produto, mas a factory pode adicionar mais lógica
            return new Produto(_idProximoProduto++, nome, preco, categoria);
        }
    }
}
