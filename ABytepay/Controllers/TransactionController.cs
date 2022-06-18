using ABytepay.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Controllers
{
    public class TransactionController
    {
        IWebDriver _web;
        List<Product> _products;

        public TransactionController(IWebDriver web, List<Product> products)
        {
            _web = web;
            _products = products;
        }

        public void Execute()
        {
            InitTransaction();
            InitInformationTransactionAsync();
        }

        void InitTransaction()
        {
            var menu = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV']"));
            if(menu != null)
                menu.Click();

            Task.Delay(200);
            var transaction = _web.FindElement(By.XPath("//div[@class='item_text'][text()='Danh sách giao dịch']"));
            transaction.Click();

            Task.Delay(200);
            var add = _web.FindElement(By.XPath("//button[@class='sc-fotPbf cVjBTV add-new'][text()=' Tạo mới']"));
            add.Click();
        }

        void InitInformationTransactionAsync()
        {
            var x = "sc-iqsfdx fYdb";

            //var name = _web.FindElement(By.(x));
            //name.Clear();
            //name.SendKeys("Nguyen Van A");
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)_web;
            js.ExecuteAsyncScript("window.scrollTo(0, document.body.scrollHeight)");

            
            Task.Delay(1000);
            var name = _web.FindElement(By.XPath("//input[@name='nameCustomer'][@class='sc-iqsfdx fYdb']"));
            name.SendKeys("Nguyen Van A");

            var phone = _web.FindElement(By.Name("mobileCustomer"));
            phone.SendKeys("0123456789");

            var address = _web.FindElement(By.Name("address"));
            address.SendKeys("Cau giay, Ha Noi, Viet Nam");

            var email = _web.FindElement(By.Name("email"));
            email.SendKeys("nguyenvana@gmail.com");
        }
    }
}
