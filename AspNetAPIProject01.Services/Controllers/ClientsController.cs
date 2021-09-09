using AspNetAPIProject01.Data.Entities;
using AspNetAPIProject01.Data.Interfaces;
using AspNetAPIProject01.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetAPIProject01.Services.Controllers
{
    [Route("api/[controller]")] //service adress api/clients
    [ApiController] //Defines controller type as API
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost]
        public IActionResult Post(ClientRegisterModel model) 
        {
            try
            {
                //create client object
                var client = new Client();
                client.Name = model.Name;
                client.Email = model.Email;

                _clientRepository.Create(client);

                return Ok($"Client {client.Name}, was created successfully");

            }
            catch (Exception e)
            {
                //HTTP status 500 - Internal Server Error
                return StatusCode(500);
            }
        }
    }
}
