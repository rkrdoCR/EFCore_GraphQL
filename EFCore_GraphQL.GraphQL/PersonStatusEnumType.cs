using GraphQL.Types;

namespace EFCore_GraphQL.GraphQL
{
    public class PersonStatusEnumType : EnumerationGraphType
    {
        public PersonStatusEnumType()
        {
            Name = "PersonStatus";

            AddValue("CREATED", "Created", 16);
            AddValue("ACTIVE", "Active", 32);
            AddValue("INACTIVE", "Inactive", 64);
        }
    }
}
