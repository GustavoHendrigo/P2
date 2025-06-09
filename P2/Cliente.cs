using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace P2
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public Cliente(int id, string nome, string email, string cpf)
        {
            if (id <= 0) throw new ArgumentException("Precisa ser maior que 0.", nameof(id));
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do cliente não pode estar vazio.", nameof(nome));
            if (!IsValidEmail(email)) throw new ArgumentException("Email Invalido.", nameof(email));
            if (!IsValidCpf(cpf)) throw new ArgumentException("CPF Invalido.", nameof(cpf));

            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        // Método para validar formato de e-mail
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Método privado para validar formato de CPF 
        private bool IsValidCpf(string cpf)
        {
            // Verificação de cpf simples, aceitando apenas 11 números.
            return Regex.IsMatch(cpf, @"^\d{11}$");
        }
    }
}
