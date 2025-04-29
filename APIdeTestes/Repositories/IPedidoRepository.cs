using APIdeTestes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Repositories
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
        Pedido ObterPorId(int id);
        IEnumerable<Pedido> ObterTodos();
    }

}
