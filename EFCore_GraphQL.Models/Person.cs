using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_GraphQL.Models
{
    [Table("Persons")]
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public PersonStatusEnum Status { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
