using ABytepay.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABytepay.Controllers
{
    public class PaymentController
    {
        IWebDriver _web;
        string _url;
        double _pDiscount;
        bool _isIgnore;

        public PaymentController(IWebDriver web, string url, double pDiscount, bool isIgnore)
        {
            _web = web;
            _url = url;
            _pDiscount = pDiscount;
        }

        public void Execute()
        {
            if (Main.IsStart && !Main.IsError) InitTab();
            if (Main.IsStart && !Main.IsError) GetDiscount();
            if (Main.IsStart && !Main.IsError) Payment();
        }

        void InitTab()
        {
            try
            {
                //_web.FindElement(By.CssSelector("body")).SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.LeftShift + "T");
                ((IJavaScriptExecutor)_web).ExecuteScript("window.open();");
                _web = _web.SwitchTo().Window(_web.WindowHandles.Last());
                _web.Navigate().GoToUrl(_url);
            }
            catch (Exception ex)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";
                MessageBox.Show(tab + " Can't init new tab", "Error");
                Main.IsError = true;
            }
        }

        void GetDiscount()
        {
            try
            {
                var appota = _web.FindElement(By.XPath($"//*[@id='root']/div[1]/div[2]/div[2]/div/div/div[2]"));
                appota.Click();

                _web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
                var items = _web.FindElements(By.ClassName("checkout-item"));
                if (items.Count < 6)
                {
                    MessageBox.Show("Today, voucher discount is out of order", "Information");
                }
                else
                {
                    var valueTotal = "";
                    var valueDiscount = "";

                    var labelTotal = items[2].FindElement(By.ClassName("label"));
                    Thread.Sleep(100);
                    string tmpTotal = ((WebElement)labelTotal).Text;
                    if (tmpTotal == "Giá Đơn Hàng:")
                        valueTotal = ((WebElement)items[2].FindElement(By.ClassName("value"))).Text;

                    var labelDiscount = items[3].FindElement(By.ClassName("label"));
                    Thread.Sleep(100);
                    string tmpDiscount = ((WebElement)labelDiscount).Text;
                    if (tmpDiscount == "Giảm Giá:")
                        valueDiscount = ((WebElement)items[3].FindElement(By.ClassName("value"))).Text;

                    if (!string.IsNullOrEmpty(valueTotal) && !string.IsNullOrEmpty(valueDiscount))
                    {
                        var rTotal = double.Parse(new string(valueTotal.Where(Char.IsDigit).ToArray()));
                        var rDiscount = double.Parse(new string(valueDiscount.Where(Char.IsDigit).ToArray()));

                        if (rTotal > 0 && rDiscount > 0)
                        {
                            double maxDiscount = 0;
                            if (rTotal < 800000)
                                maxDiscount = 1.0 * (_pDiscount * rTotal) / 1000;
                            else
                                maxDiscount = 1.0 * (_pDiscount * 800000) / 1000;

                            if (rDiscount >= maxDiscount)
                            {
                                //MessageBox.Show("Max value discount", "Success");
                            }
                            else // find again 
                            {
                                if (Main.IsStart == false)
                                    return;

                                var cancel = _web.FindElement(By.XPath("//button[@class='sc-fotPbf gnOitR']"));
                                cancel.Click();

                                GetDiscount();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string tab = _isIgnore ? "[Ignore Tab]" : "[Normal Tab]";
                MessageBox.Show(tab + " Can't find discount", "Information");
                Main.IsError = true;
            }
        }

        void Payment()
        {
            //var notrobot = _web.FindElement(By.XPath("//*[@id='recaptcha-anchor']/div[1]"), 10);
            //notrobot.Click();
        }
    }
}
