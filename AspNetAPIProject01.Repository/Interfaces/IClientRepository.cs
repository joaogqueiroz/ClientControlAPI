using AspNetAPIProject01.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetAPIProject01.Data.Interfaces
{
    public interface IClientRepository
    {
        void Create(Client client);
        List<Client> Read();
        void Update(Client client);
        void Delete(Client client);
        Client getByID(Guid clientID);

    }
}
