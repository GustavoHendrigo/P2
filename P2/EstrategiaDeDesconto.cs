using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class CategoriaEstrategiaDesconto : IEstrategiaDeDesconto
    {
        private readonly string _categoria;
        private readonly decimal _porcentagem;

        public CategoriaEstrategiaDesconto(string categoria, decimal porcentagem)
        {
            _categoria = categoria;
            _porcentagem = porcentagem;
        }

        public decimal CalcularDesconto(Pedido pedido)
        {
            decimal quantidadeTotalDesconto = 0;
            // Filtra itens pela categoria e aplica o desconto individualmente
            foreach (var item in pedido.Items.Where(i => i.Produto.Categoria == _categoria))
            {
                quantidadeTotalDesconto += item.PrecoTotal * _porcentagem;
            }
            return pedido.ValorTotal - quantidadeTotalDesconto;
        }
    }

    // Estratégia de desconto por quantidade total de itens (exemplo)
    public class QuantidadeEstrategiaDesconto : IEstrategiaDeDesconto
    {
        private readonly int _quantidadeMinima;
        private readonly decimal _porcentagem;

        public QuantidadeEstrategiaDesconto(int quantidadeMinima, decimal porcentagem)
        {
            _quantidadeMinima = quantidadeMinima;
            _porcentagem = porcentagem;
        }

        public decimal CalcularDesconto(Pedido pedido)
        {
            decimal quantidadeTotalDesconto = 0;
            // Se a quantidade total de itens no pedido for maior ou igual ao mínimo
            if (pedido.Items.Sum(i => i.Quantidade) >= _quantidadeMinima)
            {
                quantidadeTotalDesconto = pedido.ValorTotal * _porcentagem;
            }
            return pedido.ValorTotal - quantidadeTotalDesconto;
        }
    }

    // Estratégia de desconto para Black Friday (exemplo de desconto global)
    public class EstrategiaDescontoBlackFriday : IEstrategiaDeDesconto
    {
        private readonly decimal _porcentagem;

        public EstrategiaDescontoBlackFriday(decimal porcentagem)
        {
            _porcentagem = porcentagem;
        }

        public decimal CalcularDesconto(Pedido pedido)
        {
            // Aplica o desconto diretamente.
            return pedido.ValorTotal * (1 - _porcentagem);
        }
    }

    // Estratégia sem desconto (para casos em que nenhum desconto se aplica)
    public class EstrategiaSemDesconto : IEstrategiaDeDesconto
    {
        public decimal CalcularDesconto(Pedido pedido)
        {
            return pedido.ValorTotal; // Retorna o valor original sem alteração
        }
    }
}
