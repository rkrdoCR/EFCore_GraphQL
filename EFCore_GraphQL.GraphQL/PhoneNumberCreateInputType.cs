using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PhoneNumberCreateInputType : InputObjectGraphType
    {
        public PhoneNumberCreateInputType()
        {
            Name = "PhoneNumberInput";

            Field<NonNullGraphType<StringGraphType>>("number");
            Field<PhoneNumberClassEnumType>("phoneType");
            Field<StringGraphType>("description");
        }
    }
}
