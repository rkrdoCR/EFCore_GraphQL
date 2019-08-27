using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonCreateInputType : InputObjectGraphType
    {
        public PersonCreateInputType()
        {
            Name = "PersonInput";

            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("lastName");
            Field<ListGraphType<PhoneNumberCreateInputType>>("phoneNumbers");
            Field<PersonStatusEnumType>("status");
        }
    }
}
