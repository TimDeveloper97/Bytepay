using ABytepay.Domain;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Controllers
{
    public class LoginController
    {
        string _email, _password, _url;
        IWebDriver _web;

        public LoginController(IWebDriver web, string url, string email, string password)
        {
            _web = web;
            _url = url;
            _email = email;
            _password = password;
        }

        public void Execute()
        {
            string x = "";
            _web.Url = _url;

            var email = _web.FindElement(By.Name("email"));
            email.Clear();
            email.SendKeys(_email);

            var password = _web.FindElement(By.Name("password"));
            password.Clear();
            password.SendKeys(_password);

            _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            var login = _web.FindElement(By.XPath("//button[@type='submit'][text()='Đăng nhập']"));
            login.Click();
        }
    }
}
