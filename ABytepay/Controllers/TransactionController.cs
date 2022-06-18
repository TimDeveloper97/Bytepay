using ABytepay.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABytepay.Controllers
{
    public class TransactionController
    {
        IWebDriver _web;
        List<Product> _products;
        Receiver _receiver;

        public TransactionController(IWebDriver web, List<Product> products, Receiver receiver)
        {
            _web = web;
            _products = products;
            _receiver = receiver;
        }

        public void Execute()
        {
            InitTransaction();
            InitInformationTransaction();
            InitProductTransaction();
            GetLink();
        }

        void InitTransaction()
        {
            var menu = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV']"));
            if(menu != null)
                menu.Click();

            _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            var transaction = _web.FindElement(By.XPath("//div[@class='item_text'][text()='Danh sách giao dịch']"));
            transaction.Click();

            _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            var add = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV add-new'][text()=' Tạo mới']"));
            add.Click();
        }

        void InitInformationTransaction()
        {
            _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)_web; 
            //js.ExecuteAsyncScript("window.scrollTo(0, document.body.scrollHeight)");

            var body = _web.FindElement(By.CssSelector("body"));
            body.SendKeys(Keys.PageDown);

            //var name = _web.FindElement(By.XPath("//input[@name='nameCustomer'][@class='sc-iqsfdx fYdb']"));
            var name = _web.FindElement(By.Name("nameCustomer"));
            name.SendKeys(_receiver.Name);

            var phone = _web.FindElement(By.Name("mobileCustomer"));
            phone.SendKeys(_receiver.Phone);

            var address = _web.FindElement(By.Name("address"));
            address.SendKeys(_receiver.Address);

            var email = _web.FindElement(By.Name("email"));
            email.SendKeys(_receiver.Email);
        }

        void InitProductTransaction()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                string xpath = "";
                if (i == 0)
                    xpath = "//*[@id='form_total']/div[1]/div/div[2]/div/div[2]/div/table/tbody/tr/td/div";
                else
                    xpath = $"//*[@id='form_total']/div[1]/div/div[2]/div/div[2]/div/table/tbody/tr[{i + 1}]/td/div";

                var add = _web.FindElement(By.XPath(xpath));
                add.Click();

                var items = _web.FindElements(By.XPath("//input[@aria-autocomplete='list']"));
                items[items.Count - 1].SendKeys(_products[i].Name);
                Thread.Sleep(500);
                items[items.Count - 1].SendKeys(Keys.Tab);
            }

            for (int i = 0; i < _products.Count; i++) 
            {
                var number = _web.FindElement(By.XPath($"//input[@type='number'][@name='products[{i}].quantity']"));
                number.Clear();
                number.SendKeys(_products[i].Amount);
            }

            var body = _web.FindElement(By.CssSelector("body"));
            body.SendKeys(Keys.PageDown);

            Thread.Sleep(500);
            var create = _web.FindElement(By.XPath("//button[@type='submit'][@class='sc-fotPbf cVjBTV']"));
            create.Click();
        }

        void GetLink()
        {
            Thread.Sleep(500);
            var link = _web.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/p"));
            link.Click();
        }
    }
}
