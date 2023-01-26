using AutoMapper;
using ClientManagementSystem.Common.Dto;
using ClientManagementSystem.Common.Services;
using ClientManagementSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManagementSystem.Api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ClientDto client)
        {
            var dbclient = _mapper.Map<Client>(client);
            var createdClient = await _clientService.CreateClient(dbclient);
            return CreatedAtAction(nameof(Get), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(Guid id, [FromBody] ClientDto client)
        {
            var dbclient = _mapper.Map<Client>(client);
            if (id != client.Id)
            {
                return BadRequest();
            }
            if(await _clientService.GetClientAsync(id) == null)
            {
                return NotFound();
            }
            var updatedClient = await _clientService.UpdateClient(dbclient);
            var dtoclient = _mapper.Map<ClientDto>(updatedClient);
            return Ok(dtoclient);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedClient = await _clientService.DeleteClient(new Client { Id = id });
            if (deletedClient == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var dtoclient = _mapper.Map<ClientDto>(client);
            return Ok(client);
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<ClientDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetClientsAsync();
            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }

    }
}
