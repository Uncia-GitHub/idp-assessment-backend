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
        public void UriValidator_AbsoluteUri_true()
        {
            string[] validationRules = new[]{
           "https://www.confirmit.com/webapp/logincallback" // absolute rule - this url is allowed
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/webapp/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_AbsoluteUri_false()
        {
            string[] validationRules = new[]{
           "https://www.confirmit.com/webapp/logincallback" // absolute rule - this url is allowed
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/app/logincallback", validationRules); // expected false

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void UriValidator_wildcardUri_true()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/*/login" 
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/webapp/login", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_wildcardUri2_wrongDomain_false()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/webapp/login"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.net/webapp/login", validationRules); // expected false

            Assert.AreEqual(false, isValid);
        }


        [TestMethod]
        public void UriValidator_wildcardUri3_withFolder_true()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/*/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://login.confirmit.com/app/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_wildcardUri4_withFolder1_true()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/*/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://auth.confirmit.com/login/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_wildcardUri_pathWithSeveralFolder_true()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/*/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://app.confirmit.com/auth/folder/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_wildcardUri_withoutSubdomen_false()
        {
            string[] validationRules = new[]{
           "https://*.confirmit.com/*/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://confirmit.com/logincallback", validationRules); // expected false

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void UriValidator_literalRegexUri1_withFolder_true()
        {
            string[] validationRules = new[]{
           @"@https://([\w-]+\.)*confirmit.com/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://login.confirmit.com/app/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_literalRegexUri2_withFolder1_true()
        {
            string[] validationRules = new[]{
           @"@https://([\w-]+\.)confirmit.com/login/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://auth.confirmit.com/login/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void UriValidator_literalRegexUri_pathWithSeveralFolder_true()
        {
            string[] validationRules = new[]{
           @"@https://([\w-]+\.)*confirmit.com/*/logincallback"
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://app.confirmit.com/auth/folder/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }

        // TODO: is uri without domain
        //[TestMethod]
        //public void UriValidator_literalRegexUri_withoutSubdomen_true()
        //{
        //    string[] validationRules = new[]{
        //   @"@https://([\w-]+\.)*confirmit\.com\/logincallback"
        //   };

        //    var validator = new UriValidator();
        //    var isValid = validator.Validate("https://confirmit.com/logincallback", validationRules); // expected true

        //    Assert.AreEqual(true, isValid);
        //}

        [TestMethod]
        public void UriValidator_literalRegexUri_wrongdomain_false()
        {
            string[] validationRules = new[]{
           @"@https://([\w-]+\.)*confirmit\.com\/logincallback"
           };
            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.org/logincallback", validationRules); // expected false

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void UriValidator_setOfRules_true()
        {
            string[] validationRules = new[]{
           "https://www.confirmit.com/webapp/logincallback", // absolute rule - this url is allowed
        "https://*.confirmit.com/*/logincallback", // wildcard rule
        "https://*.confirmit.com/webapp/login", // wildcard rule
        @"@https://([\w-]+\.)*confirmit\.com\/logincallback" // literal regex rule	
           };

            var validator = new UriValidator();
            var isValid = validator.Validate("https://www.confirmit.com/webapp/logincallback", validationRules); // expected true

            Assert.AreEqual(true, isValid);
        }
    }
}
