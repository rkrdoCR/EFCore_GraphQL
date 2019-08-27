using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_GraphQL.Models
{
    [Table("PhoneNumbers")]
    public class PhoneNumber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Person Owner { get; set; }
        public string Number { get; set; }
        public PhoneNumberClassEnum PhoneNumberClass { get; set; }
        public string Description { get; set; }
    }
}
