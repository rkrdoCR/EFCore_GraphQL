using EFCore_GraphQL.Models;
using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonEventType : ObjectGraphType<PersonEvent>
    {
        public PersonEventType()
        {
            Field(e => e.Id);
            Field(e => e.Name);
            Field(e => e.PersonId);
            Field<PersonStatusEnumType>(
                "status",
                resolve: context => context.Source.Status
                );
            Field(e => e.Timestamp);
        }
    }
}
