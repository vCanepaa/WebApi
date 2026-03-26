using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Person : BaseEntity
    {
        public string FirstName { get;set; }
        public string LastName {  get;set; }

        public string Address { get;set; }

        public string Gender { get;set; }
    }
}
