using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public interface ILogService
    {
        void Log(string message); // Para mensagens informativas
        void LogError(string message, Exception ex = null); // Para mensagens de erro, com opcional exceção
    }
}
