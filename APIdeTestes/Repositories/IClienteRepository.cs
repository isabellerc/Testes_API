using APIdeTestes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIdeTestes.Repositories
{
    public interface IClienteRepository
    {
        void Adicionar(Cliente cliente);
        Cliente ObterPorId(int id);
        IEnumerable<Cliente> ObterTodos();
        //criar método EmailExiste e implementar em ClientRepository
        //

        
            bool EmailExiste(string email); // <<<<<< ADICIONE ISSO
        

    }

}
