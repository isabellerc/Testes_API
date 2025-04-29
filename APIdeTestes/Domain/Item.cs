using System;
using APIdeTestes.Domain;

namespace APIdeTestes.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public decimal Total => Produto.Valor * Quantidade;

        public Item(int id, int pedidoId, Produto produto, int quantidade)
        {
            Id = id;
            PedidoId = pedidoId;
            Produto = produto;
            Quantidade = quantidade;
        }
    }
}
