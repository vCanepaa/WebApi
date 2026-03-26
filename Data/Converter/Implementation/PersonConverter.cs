using WebApi.Data.Converter.Contract;
using WebApi.Data.DTO;
using WebApi.Models;

namespace WebApi.Data.Converter.Implementation
{
    public class PersonConverter : IParser<Person, PersonDto>, IParser<PersonDto, Person>
    {
        public PersonDto Parse(Person origin)
        {
            if (origin == null)
                return null;

            return new PersonDto
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender
            };
        }

        public Person Parse(PersonDto origin)
        {
            if (origin == null)
                return null;

            return new Person
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender
            };
        }

        public List<PersonDto> ParseList(List<Person> originList)
        {
            if (originList == null)
                return null;
            if (originList.Count == 0)
                return new List<PersonDto>();

            return originList.Select(x => Parse(x)).ToList();    


        }

        public List<Person> ParseList(List<PersonDto> originList)
        {
            if (originList == null)
                return null;
            if (originList.Count == 0)
                return new List<Person>();

            return originList.Select(x => Parse(x)).ToList();
        }
    }
}
