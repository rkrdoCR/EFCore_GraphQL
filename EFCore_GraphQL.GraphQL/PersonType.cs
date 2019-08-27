using EFCore_GraphQL.Models;
using GraphQL.Types;
using System.Linq;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field(p => p.Id);
            Field(p => p.Name);
            Field(p => p.LastName);

            Field<PersonStatusEnumType>(
                    "personStatus",
                    resolve: context => context.Source.Status
                );

            Field<ListGraphType<PhoneNumberType>>(
                    "phoneNumbers",
                    resolve: context => context.Source.PhoneNumbers
                        .Where(n => n.OwnerId == context.Source.Id)
                );
        }
    }
}
