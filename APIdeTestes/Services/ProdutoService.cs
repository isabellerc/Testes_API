using APIdeTestes.Domain;
using APIdeTestes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void AdicionarProduto(Produto produto)
        {
            if (produto == null || produto.Estoque < 0)
            {
                throw new ArgumentException("O produto é inválido ou o estoque está incorreto.");
            }

            _produtoRepository.Adicionar(produto);
        }

        public void AtualizarEstoque(int produtoId, int quantidade)
        {
            var produto = _produtoRepository.ObterPorId(produtoId);

            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produto.AtualizarEstoque(quantidade); // Agora você chama o método de atualização de estoque
            _produtoRepository.Atualizar(produto);
        }


        public Produto ObterProdutoPorId(int id)
        {
            return _produtoRepository.ObterPorId(id) ?? throw new KeyNotFoundException("Produto não encontrado.");
        }
    }

}
