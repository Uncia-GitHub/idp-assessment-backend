using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Helper;

namespace AssessmentTests
{
    [TestClass]
    public class UriValidatorTests
    {
        string[] validationRules = new[]{
           "https://www.confirmit.com/webapp/logincallback", // absolute rule - this url is allowed
        "https://*.confirmit.com/*/logincallback", // wildcard rule
        "https://*.confirmit.com/webapp/login", // wildcard rule
        @"@https://([\w-]+\.)*confirmit\.com\/logincallback" // literal regex rule	
           };


        [TestMethod]
        public void UriValidator_AbsoluteUri1()
        {

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/webapp/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }
    }
}
