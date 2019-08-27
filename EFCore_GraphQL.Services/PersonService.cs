using EFCore_GraphQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCore_GraphQL.Services
{
    public class PersonService : IPersonService
    {
        private IPersonEventService _eventsService;

        public PersonService(IPersonEventService eventsService)
        {
            _eventsService = eventsService;
        }

        public Task<Person> GetPersonByIdAsync(int id)
        {
            Person person = null;
            using (var context = new EFCore_GraphQLDbContext())
            {
                person = context.Persons.Include(p => p.PhoneNumbers)
                    .SingleOrDefault(p => p.Id == id);
            }

            return Task.FromResult(person);
        }

        public Task<IEnumerable<Person>> GetPersonsAsync()
        {
            List<Person> persons = new List<Person>();
            using (var context = new EFCore_GraphQLDbContext())
            {
                persons = context.Persons
                    .Include(p => p.PhoneNumbers).ToList();
            }

            return Task.FromResult(persons.AsEnumerable());
        }

        public Task<Person> CreateAsync(Person person)
        {
            using (var context = new EFCore_GraphQLDbContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }

            //add event
            var personEvent = new PersonEvent()
            {
                Id = 1,
                Name = person.Name,
                Status = person.Status,
                Timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
            };
            _eventsService.AddEvent(personEvent);

            return Task.FromResult(person);
        }

        public Task<Person> UpdateAsync(int id, PersonStatusEnum status)
        {
            Person person = null;
            using (var context = new EFCore_GraphQLDbContext())
            {
                person = context.Persons.Include(p => p.PhoneNumbers)
                    .SingleOrDefault(p => p.Id == id);

                person.Status = status;
                context.SaveChanges();
            }

            //add event
            var personEvent = new PersonEvent()
            {
                Id = 2,
                Name = person.Name,
                Status = person.Status,
                Timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
            };
            _eventsService.AddEvent(personEvent);

            return Task.FromResult(person);
        }
    }
}
