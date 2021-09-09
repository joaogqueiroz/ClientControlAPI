﻿using AspNetAPIProject01.Data.Entities;
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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var clients = _clientRepository.Read();

                return Ok(clients);

            }
            catch (Exception e)
            {
                //HTTP status 500 - Internal Server Error
                return StatusCode(500);
            }
        }
        [HttpPut]
        public IActionResult Put(ClientEditModel model)
        {
            try
            {
                if (_clientRepository.getByID(model.ClientID) != null)
                {
                    //create client object
                    var client = new Client();
                    client.ClientID = model.ClientID;
                    client.Name = model.Name;
                    client.Email = model.Email;

                    _clientRepository.Update(client);

                    return Ok($"Client {client.Name}, was updated successfully");
                }
                else
                {
                    //HTTP status 422 - Unprocessable Entity
                    return UnprocessableEntity(@"The client is not registered on system, please verify the informed id");
                }
                

            }
            catch (Exception e)
            {
                //HTTP status 500 - Internal Server Error
                return StatusCode(500);
            }
        }
        [HttpDelete("{clientID:Guid}")]
        public IActionResult Delete(Guid clientID)
        {
            try
            {
                var client = _clientRepository.getByID(clientID);
                if (client != null)
                {
                    //create client object
                    client.ClientID = clientID;

                    _clientRepository.Delete(client);

                    return Ok($"Client {client.Name}, was deleted successfully");

                }
                else
                {
                    //HTTP status 422 - Unprocessable Entity
                    return UnprocessableEntity(@"The client is not registered on system, please verify the informed id");
                }
            }
            catch (Exception e)
            {
                //HTTP status 500 - Internal Server Error
                return StatusCode(500);
            }
        }        
        [HttpGet("{clientID:Guid}")]
        public IActionResult GetByID(Guid clientID)
        {
            try
            {
                if (_clientRepository.getByID(clientID) != null)
                {
                    var client = _clientRepository.getByID(clientID);

                    return Ok(client);
                }
                else
                {
                    //HTTP status 422 - Unprocessable Entity
                    return UnprocessableEntity(@"The client is not registered on system, please verify the informed id");
                }
            }
            catch (Exception e)
            {
                //HTTP status 500 - Internal Server Error
                return StatusCode(500);
            }
        }
    }
}
