using APIdeTestes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new List<Pedido>();

        public void Adicionar(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public Pedido ObterPorId(int id)
        {
            return _pedidos.FirstOrDefault(p => p.Id == id);  // Agora a propriedade Id está disponível
        }


        public IEnumerable<Pedido> ObterTodos()
        {
            return _pedidos;
        }
    }

}
