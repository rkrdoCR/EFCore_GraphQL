using System;

namespace EFCore_GraphQL.Models
{
    public class PersonEvent
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public PersonStatusEnum Status { get; set; }
        public string Timestamp { get; set; }

    }
}
