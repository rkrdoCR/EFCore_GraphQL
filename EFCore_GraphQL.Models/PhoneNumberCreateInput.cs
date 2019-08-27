namespace EFCore_GraphQL.Models
{
    public class PhoneNumberCreateInput
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public PhoneNumberClassEnum PhoneNumberClass { get; set; }
    }
}
