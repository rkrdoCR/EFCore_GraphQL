using EFCore_GraphQL.Models;
using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PhoneNumberType : ObjectGraphType<PhoneNumber>
    {
        public PhoneNumberType()
        {
            Field(p => p.Id);
            Field(p => p.OwnerId);
            Field(p => p.Number);
            Field(p => p.Description);

            Field<PhoneNumberClassEnumType>(
                    "phoneType",
                    resolve: context => context.Source.PhoneNumberClass
                );
        }
    }
}
