using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore_GraphQL.Models;

namespace EFCore_GraphQL.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> GetPersonsAsync();

        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(int id, PersonStatusEnum status);
    }
}