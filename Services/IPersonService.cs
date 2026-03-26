using WebApi.Data.DTO;

namespace WebApi.Services
{
    public interface IPersonService
    {

        Task<List<PersonDto>> GetPersons();

        Task<PersonDto> GetPerson(long id);

        Task<PersonDto> UpdatePerson(PersonDto person);

        Task<PersonDto> CreatePerson(PersonDto person);

        Task DeletePerson(long id);
    }
}
