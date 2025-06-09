using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace P2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("             Loja Virtual ");
            Console.WriteLine("======================================");

            ILogService logService = new ConsoleLogService();
            IRepositorioDePedidos repositorioDePedidos = new RepositorioDePedidos();
            IDescontoService descontoService = new DescontoService(logService); // DescontoService depende de ILogService
            ProdutoFactory produtoFactory = new ProdutoFactory();
            PedidoFactory pedidoFactory = new PedidoFactory();
            

            logService.Log("Dependências configuradas com sucesso.");

            Produto laptop = null;
            Produto mouse = null;
            Produto teclado = null;
            Produto cadeira = null;
            Produto headphones = null;

            Console.WriteLine("\n--- 1. Cadastro de Produtos ---");
            logService.Log("Iniciando cadastro de produtos...");
            try
            {
                laptop = produtoFactory.CriarProduto("Laptop Gamer ", 5000.00m, "Eletrônicos");
                mouse = produtoFactory.CriarProduto("Mouse Ergonômico ", 150.00m, "Acessórios");
                teclado = produtoFactory.CriarProduto("Teclado Mecânico", 300.00m, "Acessórios");
                cadeira = produtoFactory.CriarProduto("Cadeira Gamer", 1200.00m, "Mobiliário");
                headphones = produtoFactory.CriarProduto("Fone de Ouvido sem fio", 450.00m, "Eletrônicos");

                Console.WriteLine($"  - Produto Cadastrado: ID:{laptop.Id}, {laptop.Nome}, Preço: {laptop.Preco:C}, Categoria: {laptop.Categoria}");
                Console.WriteLine($"  - Produto Cadastrado: ID:{mouse.Id}, {mouse.Nome}, Preço: {mouse.Preco:C}, Categoria: {mouse.Categoria}");
                Console.WriteLine($"  - Produto Cadastrado: ID:{teclado.Id}, {teclado.Nome}, Preço: {teclado.Preco:C}, Categoria: {teclado.Categoria}");
                Console.WriteLine($"  - Produto Cadastrado: ID:{cadeira.Id}, {cadeira.Nome}, Preço: {cadeira.Preco:C}, Categoria: {cadeira.Categoria}");
                Console.WriteLine($"  - Produto Cadastrado: ID:{headphones.Id}, {headphones.Nome}, Preço: {headphones.Preco:C}, Categoria: {headphones.Categoria}");

                // Demonstração de validação de produto inválido
                logService.Log("Tentando cadastrar produto com preço inválido...");
                produtoFactory.CriarProduto("Produto de Teste Inválido", -10m, "Teste");
            }
            catch (ArgumentException ex)
            {
                logService.LogError($"Falha no cadastro de produto: {ex.Message}");
            }

            Console.WriteLine("\n--- 2. Cadastro de Clientes ---");
            logService.Log("Iniciando cadastro de clientees...");
            try
            {
                Cliente cliente1 = new Cliente(1, "João Silva", "joao.silva@email.com", "11122233344");
                Cliente cliente2 = new Cliente(2, "Maria Oliveira", "maria.o@email.com", "55566677788");
                Cliente cliente3 = new Cliente(3, "Carlos Pereira", "carlos.p@domain.com", "99988877766");

                Console.WriteLine($"  - Cliente Cadastrado: ID:{cliente1.Id}, {cliente1.Nome}, Email: {cliente1.Email}, CPF: {cliente1.Cpf}");
                Console.WriteLine($"  - Cliente Cadastrado: ID:{cliente2.Id}, {cliente2.Nome}, Email: {cliente2.Email}, CPF: {cliente2.Cpf}");
                Console.WriteLine($"  - Cliente Cadastrado: ID:{cliente3.Id}, {cliente3.Nome}, Email: {cliente3.Email}, CPF: {cliente3.Cpf}");

                // Demonstração de validação de clientee inválido
                logService.Log("Tentando cadastrar clientee com e-mail inválido...");
                new Cliente(4, "Cliente Erro", "email_invalido", "12345678900");
            }
            catch (ArgumentException ex)
            {
                logService.LogError($"Falha no cadastro de clientee: {ex.Message}");
            }

            Console.WriteLine("\n--- 3. Criação de Pedidos ---");
            logService.Log("Iniciando criação de pedidos...");
            try
            {
                var pedidoItems1 = new List<ItemPedido>
                {
                    new ItemPedido(laptop, 1),
                    new ItemPedido(mouse, 2)
                };
                Pedido pedido1 = pedidoFactory.CriarPedido(new Cliente(1, "João Silva", "joao.silva@email.com", "11122233344"), pedidoItems1);
                repositorioDePedidos.Add(pedido1);
                Console.WriteLine($"  - Pedido {pedido1.Id} criado para {pedido1.Cliente.Nome}. Total: {pedido1.ValorTotal:C}");

                var pedidoItems2 = new List<ItemPedido>
                {
                    new ItemPedido(teclado, 1),
                    new ItemPedido(cadeira, 1),
                    new ItemPedido(headphones, 3)
                };
                Pedido pedido2 = pedidoFactory.CriarPedido(new Cliente(2, "Maria Oliveira", "maria.o@email.com", "55566677788"), pedidoItems2);
                repositorioDePedidos.Add(pedido2);
                Console.WriteLine($"  - Pedido {pedido2.Id} criado para {pedido2.Cliente.Nome}. Total: {pedido2.ValorTotal:C}");
            }
            catch (ArgumentException ex)
            {
                logService.LogError($"Falha na criação do pedido: {ex.Message}");
            }

            Console.WriteLine("\n--- 4. Relatório de Pedidos ---");
            logService.Log("Gerando relatório de todos os pedidos...");
            var todosPedidos = repositorioDePedidos.GetAll();
            if (!todosPedidos.Any())
            {
                Console.WriteLine("  Nenhum pedido encontrado.");
            }
            else
            {
                foreach (var pedido in todosPedidos)
                {
                    Console.WriteLine($"\n  Pedido ID: {pedido.Id}, Cliente: {pedido.Cliente.Nome}, Data: {pedido.DataPedido:dd/MM/yyyy HH:mm}, Valor Total Original: {pedido.ValorTotal:C}");
                    Console.WriteLine("    Itens:");
                    foreach (var item in pedido.Items)
                    {
                        Console.WriteLine($"      - {item.Produto.Nome} (x{item.Quantidade}) - Preço Unitário: {item.PrecoUnitario:C}, Subtotal: {item.PrecoTotal:C}");
                    }
                }
            }

            Console.WriteLine("\n--- 5. Aplicação de Descontos ---");
            logService.Log("Demonstrando aplicação de descontos...");

            // Recupera pedidos para aplicar descontos
            Pedido pedidosRecuperados1 = repositorioDePedidos.GetById(1);
            Pedido pedidosRecuperados2 = repositorioDePedidos.GetById(2);

            if (pedidosRecuperados1 != null)
            {
                Console.WriteLine($"\n  Aplicando desconto de 10% em 'Eletrônicos' para Pedido 1 (ID: {pedidosRecuperados1.Id})...");
                IEstrategiaDeDesconto descontoCategoria = new CategoriaEstrategiaDesconto("Eletrônicos", 0.10m);
                pedidosRecuperados1.AplicarDesconto(descontoCategoria);
                descontoService.AplicarDesconto(pedidosRecuperados1, descontoCategoria);
                Console.WriteLine($"    Novo Total do Pedido 1: {pedidosRecuperados1.ValorTotal:C}");
            }

            if (pedidosRecuperados2 != null)
            {
                Console.WriteLine($"\n  Aplicando desconto de 5% por 'Quantidade' (>= 3 itens) para Pedido 2 (ID: {pedidosRecuperados2.Id})...");
                IEstrategiaDeDesconto quantidadeDesconto = new QuantidadeEstrategiaDesconto(3, 0.05m);
                pedidosRecuperados2.AplicarDesconto(quantidadeDesconto);
                descontoService.AplicarDesconto(pedidosRecuperados2, quantidadeDesconto);
                Console.WriteLine($"    Novo Total do Pedido 2: {pedidosRecuperados2.ValorTotal:C}");
            }
            else
            {
                logService.LogError("Pedido 2 não encontrado para aplicação de desconto por quantidade.");
            }

            // Demonstração de outro desconto global
            if (pedidosRecuperados1 != null)
            {
                Console.WriteLine($"\n  Aplicando desconto de 'Black Friday' (20%) para Pedido 1 (ID: {pedidosRecuperados1.Id})...");
                IEstrategiaDeDesconto descontoBlackFriday = new EstrategiaDescontoBlackFriday(0.20m);
                pedidosRecuperados1.AplicarDesconto(descontoBlackFriday);
                descontoService.AplicarDesconto(pedidosRecuperados1, descontoBlackFriday);
                Console.WriteLine($"    Novo Total do Pedido 1 (pós Black Friday): {pedidosRecuperados1.ValorTotal:C}");
            }


            Console.WriteLine("  Requisitos Técnicos e Princípios SOLID Demonstrados:");
            Console.WriteLine("- **Encapsulamento**: Propriedades privadas e acesso controlado através de métodos e construtores (ex: Produto, Cliente, Pedido).");
            Console.WriteLine("- **Polimorfismo**: Utilização da interface IEstrategiaDeDesconto, onde diferentes implementações (CategoriaEstrategiaDesconto, QuantidadeEstrategiaDesconto) são usadas de forma intercambiável pelo método AplicarDesconto.");
            Console.WriteLine("- **Clean Code**: Nomes claros, classes com responsabilidades bem definidas, validações explícitas.");
            Console.WriteLine("- **SOLID Principles:**");
            Console.WriteLine("  - **S (Single Responsibility Principle - SRP)**: Cada classe tem uma única responsabilidade. Ex: Produto gerencia dados do produto, Cliente gerencia dados do clientee, Pedido gerencia o ciclo de vida do pedido.");
            Console.WriteLine("  - **O (Open/Closed Principle - OCP)**: O sistema é aberto para extensão, mas fechado para modificação. Ex: Novas estratégias de desconto podem ser adicionadas implementando IEstrategiaDeDesconto sem modificar a classe Pedido.");
            Console.WriteLine("  - **L (Liskov Substitution Principle - LSP)**: Implementações de IEstrategiaDeDesconto podem ser substituídas entre si sem afetar o comportamento do sistema.");
            Console.WriteLine("  - **I (Interface Segregation Principle - ISP)**: Interfaces pequenas e coesas. Ex: IEstrategiaDeDesconto, ILogService, IRepositorioDePedidos.");
            Console.WriteLine("  - **D (Dependency Inversion Principle - DIP)**: Módulos de alto nível não dependem de módulos de baixo nível. Ambos dependem de abstrações. Ex: Pedido depende de IEstrategiaDeDesconto, DescontoService depende de ILogService.");
            Console.WriteLine("- **Design Patterns **:");
            Console.WriteLine("  - **Strategy Pattern**: Implementado com IEstrategiaDeDesconto e suas classes concretas para diferentes algoritmos de desconto.");
            Console.WriteLine("  - **Factory Method/Pattern**: Implementado com ProdutoFactory e PedidoFactory para encapsular a lógica de criação de objetos.");
            Console.WriteLine("  - (Adicional) **Repository Pattern**: Implementado com IRepositorioDePedidos e RepositorioDePedidos para abstrair a persistência de dados.");
            Console.WriteLine("- **Manutenção e Extensibilidade**: A modularidade, o uso de interfaces e padrões de design tornam o código mais fácil de manter, testar e estender com novas funcionalidades.");

            logService.Log("Aplicação finalizada. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}