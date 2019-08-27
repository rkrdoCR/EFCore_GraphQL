using System.Collections.Generic;

namespace EFCore_GraphQL.Models
{
    public class PersonCreateInput
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<PhoneNumberCreateInput> PhoneNumbers { get; set; }
        public PersonStatusEnum Status { get; set; }
    }
}
