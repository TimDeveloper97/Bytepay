using ABytepay.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABytepay.Controllers
{
    public class PaymentController
    {
        IWebDriver _web;
        string _url;

        public PaymentController(IWebDriver web, string url)
        {
            _web = web;
            _url = url;
        }

        public void Execute()
        {
            InitTab();
            GetDiscount();
            Payment();
        }

        void InitTab()
        {
            //_web.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "T");  
            ((IJavaScriptExecutor)_web).ExecuteScript("window.open();");
            _web = _web.SwitchTo().Window(_web.WindowHandles.Last());
            _web.Navigate().GoToUrl(_url);
        }

        void GetDiscount()
        {
            var appota = _web.FindElement(By.XPath($"//*[@id='root']/div[1]/div[2]/div[2]/div/div/div[2]"));
            appota.Click();
        }

        void Payment()
        {
            var notrobot = _web.FindElement(By.XPath("//*[@id='recaptcha-anchor']/div[1]"), 10);
            notrobot.Click();
        }
    }
}
