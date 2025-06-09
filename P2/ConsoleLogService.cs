using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    // Implementação do serviço de log que escreve no console
    public class ConsoleLogService : ILogService // Implementa a interface
    {
        public void Log(string mensagem)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:dd-MM-yyyy HH:mm:ss}: {mensagem}");
        }

        public void LogError(string mensagem, Exception ex = null)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Define a cor do texto para vermelho
            Console.WriteLine($"[ERROR] {DateTime.Now:dd-MM-yyyy HH:mm:ss}: {mensagem}");
            if (ex != null)
            {
                Console.WriteLine($"  Tipo de Exceção: {ex.GetType().Name}");
                Console.WriteLine($"  Mensagem: {ex.Message}");
                Console.WriteLine($"  Rastreamento: {ex.StackTrace}");
            }
            Console.ResetColor(); // Reseta a cor do texto
        }
    }
}
