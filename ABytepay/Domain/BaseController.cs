using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Domain
{
    public class BaseController : IBaseController
    {
        readonly string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        public static ChromeDriver _chromeDriver;
        public static FirefoxDriver _firefoxDriver;
        public static EdgeDriver _edgeDriver;

        public void InitEdge()
        {
            EdgeDriverService service = EdgeDriverService.CreateDefaultService(path + @"\Drivers\", "msedgedriver.exe");
            service.HideCommandPromptWindow = true;

            if (_edgeDriver == null)
                _edgeDriver = new EdgeDriver(service);
        }

        public void InitFirefox()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(path + @"\Drivers\", "geckodriver.exe");
            service.HideCommandPromptWindow = true;

            if (_firefoxDriver == null)
                _firefoxDriver = new FirefoxDriver(service);

        }

        public void InitChrome()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(path + @"\Drivers\", "chromedriver.exe");
            service.HideCommandPromptWindow = true;

            if (_chromeDriver == null)
                _chromeDriver = new ChromeDriver(service);
        }

        public void CloseChrome()
        {
            _chromeDriver.Close();
        }

        public void CloseFirefox()
        {
            _firefoxDriver.Close();
        }

        public void CloseEdge()
        {
            _edgeDriver.Close();
        }

        public IWebDriver GetFirefox() => _firefoxDriver;

        public IWebDriver GetChrome() => _chromeDriver;

        public IWebDriver GetEdge() => _edgeDriver;
    }
}
