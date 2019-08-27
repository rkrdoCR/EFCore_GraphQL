using EFCore_GraphQL.Services;
using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonsQuery : ObjectGraphType<object>
    {
        public PersonsQuery(IPersonService service)
        {
            Name = "Query";

            Field<ListGraphType<PersonType>>(
                    "persons",
                    resolve: context => service.GetPersonsAsync()
                );

            Field<PersonType>(
                "person",
                arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" }),
                  resolve: context =>
                  {
                      var id = context.GetArgument<int>("id");
                      return service.GetPersonByIdAsync(id);
                  }
            );
        }
    }
}
