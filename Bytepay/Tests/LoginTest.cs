using Bytepay.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bytepay.Methods
{
    [TestClass]
    class LoginTest : BaseUnitTest
    {
        string _email, _password, _url;

        [TestInitialize]
        public void LoginInitialize()
        {
            _email = "db05111997@gmail.com";
            _password = "123456789";
            _url = "https://bytepay.vn/login";
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _edgeDriver.Url = _url;
            
            var email = _edgeDriver.FindElementByName("email");
            email.Clear();
            email.SendKeys(_email + "1");

            var password = _edgeDriver.FindElementByName("password");
            password.Clear();
            password.SendKeys(_password);
        }

        [TestCleanup]
        public void LoginCleanup()
        {
            
        }
    }
}
