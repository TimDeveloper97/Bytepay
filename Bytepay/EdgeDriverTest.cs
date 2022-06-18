using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;

namespace Bytepay
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {

            // Initialize edge driver 
            //var service = EdgeDriverService.CreateDefaultService();
            //service.UseVerboseLogging = true;
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            if (_driver == null)
                _driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(path + @"\Drivers\", "msedgedriver.exe"));
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://www.bing.com";
            Assert.AreEqual("Bing", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
