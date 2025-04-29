using APIdeTestes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new List<Cliente>();

        public void Adicionar(Cliente cliente)
        {
            _clientes.Add(cliente);
        }

        public Cliente ObterPorId(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _clientes;
        }

        // >>>> Novo método implementado conforme pedido
        public bool EmailExiste(string email)
        {
            return _clientes.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
