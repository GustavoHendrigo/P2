using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public string Categoria { get; private set; }

        public Produto(int id, string nome, decimal preco, string categoria)
        {
            // Validações no construtor para garantir que o objeto seja sempre válido (Design by Contract)
            if (id <= 0) throw new ArgumentException("Precisa ser maior que 0.", nameof(id));
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do produto não pode estar vazio.", nameof(nome));
            if (preco <= 0) throw new ArgumentException("O preço precisa ser maior que 0.", nameof(preco));
            if (string.IsNullOrWhiteSpace(Categoria)) throw new ArgumentException("Categoria não pode estar vazia.", nameof(categoria));

            Id = id;
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
        }

        // Métodos para alteração controlada (exemplo de SRP)
        public void PrecoAtualizado(decimal novoPreco)
        {
            if (novoPreco <= 0) throw new ArgumentException("Novo preço precisa ser maior que 0.", nameof(novoPreco));
            Preco = novoPreco;
        }

        public void CategoriaAtualizada(string novaCategoria)
        {
            if (string.IsNullOrWhiteSpace(novaCategoria)) throw new ArgumentException("Categoria não pode estar vazia.", nameof(novaCategoria));
            Categoria = novaCategoria;
        }
    }
}
