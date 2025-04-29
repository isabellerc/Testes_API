using System;

namespace APIdeTestes.Domain
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }  // Propriedade para armazenar o valor do produto
        public int Estoque { get; private set; }

        // Construtor
        public Produto(int id, string nome, decimal valor, int estoque)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
            Estoque = estoque;
        }

        public void ReduzirEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            if (quantidade > Estoque)
                throw new InvalidOperationException("Estoque insuficiente.");

            Estoque -= quantidade;
        }

        public void AtualizarEstoque(int quantidade)
        {
            if (quantidade < 0 && Math.Abs(quantidade) > Estoque)
            {
                throw new InvalidOperationException("Não é possível reduzir o estoque para um valor negativo.");
            }

            Estoque += quantidade;
        }
    }
}
