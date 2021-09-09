using AspNetAPIProject01.Data.Entities;
using AspNetAPIProject01.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AspNetAPIProject01.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionstring;

        public ClientRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Create(Client client)
        {
            var query = @"
                INSERT INTO CLIENT_TB(
                    CLIENTID,
                    NAME,
                    EMAIL, 
                    REGISTRATIONDATE)
                VALUES(
                    NEWID(),
                    @Name,
                    @Email,
                    GETDATE())";
            using (var connection = new SqlConnection(_connectionstring)) 
            {
                connection.Execute(query, client);
            }

        }

        public List<Client> Read()
        {
            var query = @"
                SELECT * FROM CLIENT_TB
                ORDER BY NAME
            ";
            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection.Query<Client>(query).ToList();
            }
        }
        public void Update(Client client)
        {
                var query = @"
                UPDATE CLIENT_TB
                    NAME = @Name,
                    EMAIL = @Email
                WHERE CLIENTID = @ClientID";
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Execute(query, client);
                }
            
        }

        public void Delete(Client client)
        {
            var query = @"
                DELETE CLIENT_TB
                WHERE CLIENTID = @ClientID";
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, client);
            }
        }

        public Client getByID(Guid clientID)
        {
            var query = @"
                SELECT * FROM CLIENT_TB
                WHERE CLIENTID = @clientID
            ";
            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection.Query<Client>(query, new { clientID }).FirstOrDefault();
            }
        }
    }
}
