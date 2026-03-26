using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Data.DTO;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("person")]
    public class PersonController : Controller
    {
        private IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]

        public async Task<IActionResult> Get([FromRoute] long id) {


            _logger.LogInformation("Fetching person with ID: {id}",id);

            var personInDb = await _personService.GetPerson(id);
             
            if(personInDb == null)
            {
                _logger.LogWarning("Person not found");

                return NotFound("Person not found");
            }

            _logger.LogDebug("{id} Person Returned", id);

            return Ok(personInDb);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching all persons");
            var personInDb = await _personService.GetPersons();

            if (personInDb == null || !personInDb.Any())
            {
                _logger.LogWarning("Person not found");

                return NotFound("Pessoa não existe");
            }

            return Ok(personInDb);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto person)
        {
            var personInDb = await _personService.GetPerson(person.Id);
            if (personInDb == null) {
               await _personService.CreatePerson(person);
                _logger.LogDebug("{id} Person Created", person.Id);

                return Ok(person);
            }


            return BadRequest("Pessoa já existente");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDto person)
        {

            _logger.LogInformation("Update person with ID{id}", person.Id);

            var personInDb = await _personService.GetPerson(person.Id);
            if (personInDb != null)
            {
                 await _personService.UpdatePerson(person);
                _logger.LogDebug("{id} Person updated", person.Id);

                return Ok(person);
            }
            _logger.LogError("{id} Id not Found", person.Id);

            return NotFound("Pessoa não existe");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var personInDb = await _personService.GetPerson(id);
            if (personInDb != null)
            {
                await _personService.DeletePerson(id);
                return Ok("Deletado Com sucesso");
            }
            _logger.LogWarning("{id} Id not Found", id);

            return NotFound("Pessoa não existe");
        }

    }
}
