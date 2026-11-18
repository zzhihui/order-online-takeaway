using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Test.Utilities.RegexDefinedTypes
{
    public class PersonType
    {
        [RegularExpression("^([a-zA-Z '-]{1,40})$")]
        public string FirstName { get; set; }


        [RegularExpression("^([a-zA-Z '-]{1,40})$")]
        public string LastName { get; set; }


        [RegularExpression("^[a-z0-9_-]{3,16}$")]
        public string UserName { get; set; }


        public DateTime DOB { get; set; }


    }
}