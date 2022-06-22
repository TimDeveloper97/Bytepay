using ABytepay.Domain;
using ABytepay.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABytepay.Controllers
{
    public class LoginController
    {
        string _email, _password, _url;
        bool _isIgnore;
        IWebDriver _web;

        public LoginController(IWebDriver web, string url, string email, string password, bool isIgnore)
        {
            _web = web;
            _url = url;
            _email = email;
            _password = password;
            _isIgnore = isIgnore;
        }

        public void Execute()
        {
            try
            {
                _web.Url = _url;

                _web.FindElement(By.XPath("//*[@id='root']/div[1]/div[2]/div/div[2]/div/div/div/div/h2"), 15);

                var email = _web.FindElement(By.Name("email"));
                email.Clear();
                email.SendKeys(_email);

                var password = _web.FindElement(By.Name("password"));
                password.Clear();
                password.SendKeys(_password);

                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
                Thread.Sleep(500);
                var login = _web.FindElement(By.XPath("//button[@type='submit'][text()='Đăng nhập']"));

                login.Click();
                while (true)
                {
                    Thread.Sleep(500);
                    if (_web.FindElement(By.XPath("//*[@id='root']/div[1]/header/div[2]/div[1]/div[1]"), 10))
                        break;
                }

            }
            catch (Exception)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";
                MessageBox.Show($"{tab} Can't login to account", "Error");
                Main.IsError = true;
            }
        }
    }
}
