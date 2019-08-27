using EFCore_GraphQL.Models;
using GraphQL.Types;
using System;

namespace EFCore_GraphQL.GraphQL
{
    public class PhoneNumberClassEnumType : EnumerationGraphType
    {
        public PhoneNumberClassEnumType()
        {
            Name = "PhoneNumberClass";

            AddValue("HOME", "Home phone number", 2);
            AddValue("MOBILE", "Mobile phone number", 4);
            AddValue("WORK", "Work phone number", 8);
        }

        //public override object ParseValue(object value)
        //{
        //    var xxx = Enum.Parse<PhoneNumberClassEnum>(value.ToString());

        //    return xxx;
        //}
    }
}
