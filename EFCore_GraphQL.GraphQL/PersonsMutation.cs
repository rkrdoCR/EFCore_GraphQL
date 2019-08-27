using EFCore_GraphQL.Models;
using EFCore_GraphQL.Services;
using GraphQL.Types;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonsMutation : ObjectGraphType<object>
    {
        public PersonsMutation(IPersonService service)
        {
            Name = "Mutation";
            Description = "Persons mutation";

            Field<PersonType>(
                "createPerson",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PersonCreateInputType>>
                    {
                        Name = "person"
                    }
                ),
                resolve: context => PersonDelegate.CreatePerson(context, service)
                // for some reason the code below does not properly map the phones enum
                //    context =>
                //{
                //    // var personInput = (context.Arguments["person"] as Dictionary<string, object>);
                //    // ((context.Arguments["person"] as Dictionary<string, object>)["phoneNumbers"] as List<object>)
                //    
                //    var personInput = context.GetArgument<Person>("person");

                //    var person = new Person()
                //    {
                //        Name = personInput.Name,
                //        LastName = personInput.LastName,
                //        Status = personInput.Status
                //    };
                //    person.PhoneNumbers = personInput.PhoneNumbers.Select(
                //        n => new PhoneNumber()
                //        {
                //            Owner = person,
                //            Number = n.Number,
                //            Description = n.Description ?? string.Empty,
                //            PhoneNumberClass = n.PhoneNumberClass
                //        }).ToList();

                //    return service.CreateAsync(person);
                //}
            );

            Field<PersonType>(
                "updatePersonStatus",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<PersonStatusEnumType>>
                    {
                        Name = "status"
                    }
                ),
                resolve: context =>
                {
                    var personId = context.GetArgument<int>("id");
                    var status = context.GetArgument<PersonStatusEnum>("status");

                    return service.UpdateAsync(personId, status);
                }
            );
        }

        public class PersonDelegate
        {
            public static Person CreatePerson(ResolveFieldContext<object> context, IPersonService service)
            {
                string output = JsonConvert.SerializeObject(context.Arguments["person"]);
                dynamic personInput = JObject.Parse(output);
                IEnumerable<dynamic> phoneNumbers = personInput.phoneNumbers;

                var person = new Person()
                {
                    Name = personInput.name,
                    LastName = personInput.lastName,
                    Status = personInput.status ?? PersonStatusEnum.CREATED
                };
                person.PhoneNumbers = phoneNumbers.Select(
                    n => new PhoneNumber()
                    {
                        Owner = person,
                        Number = n.number,
                        Description = n.description ?? string.Empty,
                        PhoneNumberClass = n.phoneType ?? PhoneNumberClassEnum.HOME
                    }).ToList();

                return service.CreateAsync(person).Result;
            }
        }
    }
}
