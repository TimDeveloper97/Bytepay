using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bytepay.Domain
{
    public class BaseUnitTest
    {
        static readonly string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        protected ChromeDriver _chromeDriver;
        protected FirefoxDriver _firefoxDriver;
        protected EdgeDriver _edgeDriver;

        public BaseUnitTest()
        {
            if (_chromeDriver == null)
                _chromeDriver = new ChromeDriver(path + @"\Drivers\");

            if (_firefoxDriver == null)
                _firefoxDriver = new FirefoxDriver(path + @"\Drivers\");

            if (_edgeDriver == null)
                _edgeDriver = new EdgeDriver(EdgeDriverService.CreateDefaultService(path + @"\Drivers\", "msedgedriver.exe"));
        }
    }
}
