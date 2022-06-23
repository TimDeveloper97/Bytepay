using ABytepay.Helpers;
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
        Product _product;
        Receiver _receiver;
        bool _isIgnore;
        static Mutex _mutex = new Mutex();

        public TransactionController(IWebDriver web, Product product, Receiver receiver, bool isIgnore)
        {
            _web = web;
            _product = product;
            _receiver = receiver;
            _isIgnore = isIgnore;
        }

        public void Execute()
        {
            if (Main.IsStart && !Main.IsError) InitTransaction();
            if (Main.IsStart && !Main.IsError) InitInformationTransaction();
            if (Main.IsStart && !Main.IsError) InitProductTransaction();
            GetLink();
        }

        void InitTransaction()
        {
            try
            {
                _web.FindElement(By.ClassName("logo"), 15);

                var body = _web.FindElement(By.CssSelector("body"));
                body.SendKeys(Keys.PageUp);

                var menu = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV']"));
                if (menu != null)
                    menu.Click();
            }
            catch (Exception)
            {
            }

            try
            {
                _web.FindElement(By.XPath("//div[@class='item_text'][text()='Danh sách giao dịch']"), 15);

                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
                var transaction = _web.FindElement(By.XPath("//div[@class='item_text'][text()='Danh sách giao dịch']"));
                transaction.Click();

                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
                var add = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV add-new'][text()=' Tạo mới']"));
                add.Click();
            }
            catch (Exception)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";
                System.Windows.Forms.MessageBox.Show(tab + " Can't find transaction", "Error");
                Main.IsError = true;
            }
        }

        void InitInformationTransaction()
        {
            try
            {

                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
                //IJavaScriptExecutor js = (IJavaScriptExecutor)_web; 
                //js.ExecuteAsyncScript("window.scrollTo(0, document.body.scrollHeight)");

                var body = _web.FindElement(By.CssSelector("body"));
                body.SendKeys(Keys.PageDown);

                //wait
                _web.FindElement(By.Name("nameCustomer"), 15);

                var name = _web.FindElement(By.Name("nameCustomer"));
                name.SendKeys(_receiver.Name);

                var phone = _web.FindElement(By.Name("mobileCustomer"));
                phone.SendKeys(_receiver.Phone);

                var address = _web.FindElement(By.Name("address"));
                address.SendKeys(_receiver.Address);

                var email = _web.FindElement(By.Name("email"));
                email.SendKeys(_receiver.Email);
            }
            catch (Exception)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";
                System.Windows.Forms.MessageBox.Show(tab + " Can't fill information user", "Error");
                Main.IsError = true;
            }
        }

        void InitProductTransaction()
        {
            try
            {
                // add product
                int p = 0;
                if (!string.IsNullOrEmpty(_product.Name1))
                    AddProduct(p++, _product.Name1);

                if (!string.IsNullOrEmpty(_product.Name2))
                    AddProduct(p++, _product.Name2);

                // add amount
                p = 0;
                if (!string.IsNullOrEmpty(_product.Name1))
                    AddAmount(p++, _product.Amount1);

                if (!string.IsNullOrEmpty(_product.Name2))
                    AddAmount(p++, _product.Amount2);

                //click create link
                var body = _web.FindElement(By.CssSelector("body"));
                body.SendKeys(Keys.PageDown);

                Thread.Sleep(500);
                var create = _web.FindElement(By.XPath("//button[@type='submit'][@class='sc-fotPbf cVjBTV']"));
                create.Click();
            }
            catch (Exception)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";

                System.Windows.Forms.MessageBox.Show(tab + " Can't add items in cart", "Error");
                Main.IsError = true;
            }
        }

        void AddProduct(int position, string name)
        {
            string xpath;
            if (position == 0)
                xpath = "//*[@id='form_total']/div[1]/div/div[2]/div/div[2]/div/table/tbody/tr/td/div";
            else
                xpath = $"//*[@id='form_total']/div[1]/div/div[2]/div/div[2]/div/table/tbody/tr[{position + 1}]/td/div";

            var add = _web.FindElement(By.XPath(xpath));
            add.Click();

            // add product
            var items = _web.FindElements(By.XPath("//input[@aria-autocomplete='list']"));
            items[items.Count - 1].SendKeys(name);

            _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
            Thread.Sleep(2000);

            //new Actions(_web).SendKeys(Keys.Tab); 
            items[items.Count - 1].SendKeys(Keys.Tab);
        }

        void AddAmount(int position, string amount)
        {

            // add amount
            var number = _web.FindElement(By.XPath($"//input[@type='number'][@name='products[{position}].quantity']"));
            number.Clear();
            number.SendKeys(amount);
        }

        void GetLink()
        {
            try
            {
                _mutex.WaitOne();
                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
                Thread.Sleep(500);

                //wait
                _web.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/p"), 15);

                //if (_isIgnore)
                //{
                //    _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
                //    Thread.Sleep(2000);
                //}

                var link = _web.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/p"));
                link.Click();

                _mutex.ReleaseMutex();
            }
            catch (Exception)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";

                System.Windows.Forms.MessageBox.Show(tab + " Can't get link", "Error");
                Main.IsError = true;
            }
        }
    }
}
