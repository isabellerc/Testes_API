using APIdeTestes.Domain;
using APIdeTestes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool AdicionarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentException("O cliente não pode ser nulo.");
            }

            _clienteRepository.Adicionar(cliente);
            return true;
        }

        public Cliente ObterClientePorId(int id)
        {
            return _clienteRepository.ObterPorId(id) ?? throw new KeyNotFoundException("Cliente não encontrado.");
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return _clienteRepository.ObterTodos();
        }
    }

}
