using System.Threading.Tasks;
using WebApi.Data.Converter.Implementation;
using WebApi.Data.DTO;
using WebApi.DataBase;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;

        private readonly PersonConverter _converter;
        public PersonService(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public async Task DeletePerson(long id)
        {
           await _repository.Delete(id);
        }

        public async Task<PersonDto> CreatePerson(PersonDto personDto)
        {
            var person = _converter.Parse(personDto);

            var createdPerson = await _repository.Create(person);

            return _converter.Parse(createdPerson);
        }

        public async Task<PersonDto> GetPerson(long id)
        {
            var personInDb = await _repository.GetById(id);
            return _converter.Parse(personInDb);
        }

        public async Task<List<PersonDto>> GetPersons()
        {
            var list = await _repository.GetAll();

            return _converter.ParseList(list);
        }
        public async Task<PersonDto> UpdatePerson(PersonDto personDto)
        {
            var person = _converter.Parse(personDto);

            var personUpdated = await _repository.Update(person);

            return _converter.Parse(personUpdated);
        }
    }
}
