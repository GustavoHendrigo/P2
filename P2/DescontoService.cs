using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class DescontoService : IDescontoService
    {
        private readonly ILogService _logService; // Dependência do serviço de log

        public DescontoService(ILogService logService)
        {
            _logService = logService;
        }

        public decimal AplicarDesconto(Pedido pedido, IEstrategiaDeDesconto estrategia)
        {
            // Registra a aplicação da estratégia de desconto
            _logService.Log($"Applying discount strategy: {estrategia.GetType().Name} to Order ID: {pedido.Id}");

            decimal novoTotal = estrategia.CalcularDesconto(pedido); // Calcula o novo total usando a estratégia

            // Registra o resultado da aplicação do desconto
            _logService.Log($"Order ID: {pedido.Id} - Old Total: {pedido.ValorTotal:C}, New Total: {novoTotal:C}");

            // O Pedido já atualiza seu ValorTotal, aqui apenas retornamos para fins de log ou outras operações
            return novoTotal;
        }
    }
}
