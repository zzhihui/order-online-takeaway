using System;
using Core.Common.Validation;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.UnitTests.Validation
{
    [TestClass]
    public class GuardTest
    {
        [TestMethod]
        public void NotNull_Success()
        {

           var a = new object();
            Guard.NotNull(a);

        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NotNull_Null()
        {
            object a = null;
            Guard.NotNull(a);
        }


        [TestMethod]
        public void GreaterThanZero_Pass()
        {
            Guard.GreaterThanZero(1);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GreaterThanZero_Zero()
        {
            
            Guard.GreaterThanZero(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GreaterThanZero_Negative()
        {
            Guard.GreaterThanZero(-1);
        }
    }
}
