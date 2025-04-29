using APIdeTestes.Domain;
using APIdeTestes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public void CriarPedido(Pedido pedido)
        {
            if (pedido == null || !pedido.Itens.Any())
            {
                throw new ArgumentException("O pedido deve conter itens.");
            }

            foreach (var item in pedido.Itens)
            {
                var produto = _produtoRepository.ObterPorId(item.Produto.Id);

                if (produto == null || produto.Estoque < item.Quantidade)
                {
                    throw new InvalidOperationException("Produto inválido ou estoque insuficiente.");
                }

                produto.ReduzirEstoque(item.Quantidade);
                _produtoRepository.Atualizar(produto);
            }

            _pedidoRepository.Adicionar(pedido);
        }

        public Pedido ObterPedidoPorId(int id)
        {
            return _pedidoRepository.ObterPorId(id) ?? throw new KeyNotFoundException("Pedido não encontrado.");
        }
    }

}
