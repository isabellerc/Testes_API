using APIdeTestes.Domain;
using System.Collections.Generic;
using System;

public class Pedido
{
    public int Id { get; private set; } // Adicionando a propriedade Id
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public DateTime DataPedido { get; private set; }
    public List<Item> Itens { get; private set; }

    public Pedido(int id, int clienteId, Cliente cliente, DateTime dataPedido)
    {
        Id = id;  // Inicializa o Id
        ClienteId = clienteId;
        Cliente = cliente;
        DataPedido = dataPedido;
        Itens = new List<Item>();
    }


    // Construtor alternativo que define DataPedido automaticamente como DateTime.Now
    public Pedido(int id, int clienteId, Cliente cliente)
        : this(id, clienteId, cliente, DateTime.Now)
    {
    }

    // Método para adicionar item ao pedido
    public void AdicionarItem(Produto produto, int quantidade)
    {
        Itens.Add(new Item(0, 0, produto, quantidade));
    }

    // Método para calcular o valor total do pedido
    public decimal CalcularValorTotal()
    {
        decimal total = 0;
        foreach (var item in Itens)
        {
            total += item.Total;
        }
        return total;
    }
}
