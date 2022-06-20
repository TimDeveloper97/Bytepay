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
using System.Windows.Forms;

namespace ABytepay.Domain
{
    public class BaseController : IBaseController
    {
        string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");

        public static ChromeDriver _chromeDriver;
        public static FirefoxDriver _firefoxDriver;
        public static EdgeDriver _edgeDriver;

        public void InitEdge()
        {
            try
            {
                EdgeDriverService service = EdgeDriverService.CreateDefaultService(path + @"\Drivers\", "msedgedriver.exe");
                service.HideCommandPromptWindow = true;

                if (_edgeDriver == null)
                    _edgeDriver = new EdgeDriver(service);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void InitFirefox()
        {
            try
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(path + @"\Drivers\", "geckodriver.exe");
                service.HideCommandPromptWindow = true;

                if (_firefoxDriver == null)
                    _firefoxDriver = new FirefoxDriver(service);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        public void InitChrome()
        {
            try
            {
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(path + @"\Drivers\", "chromedriver.exe");
                service.HideCommandPromptWindow = true;

                if (_chromeDriver == null)
                    _chromeDriver = new ChromeDriver(service);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void CloseChrome()
        {
            _chromeDriver?.Close();
        }

        public void CloseFirefox()
        {
            _firefoxDriver?.Close();
        }

        public void CloseEdge()
        {
            _edgeDriver?.Close();
        }

        public IWebDriver GetFirefox() => _firefoxDriver;

        public IWebDriver GetChrome() => _chromeDriver;

        public IWebDriver GetEdge() => _edgeDriver;
    }

    public class BaseIgnoreController : IBaseController
    {
        string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");

        public static ChromeDriver _chromeDriver;
        public static FirefoxDriver _firefoxDriver;
        public static EdgeDriver _edgeDriver;

        [Obsolete]
        public void InitEdge()
        {
            try
            {
                EdgeOptions options = new EdgeOptions();
                options.AddArgument("inprivate");

                EdgeDriverService service = EdgeDriverService.CreateDefaultService(path + @"\Drivers\", "msedgedriver.exe");
                service.HideCommandPromptWindow = true;

                if (_edgeDriver == null)
                    _edgeDriver = new EdgeDriver(service, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void InitFirefox()
        {
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                options.AddArgument("-private");

                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(path + @"\Drivers\", "geckodriver.exe");
                service.HideCommandPromptWindow = true;

                if (_firefoxDriver == null)
                    _firefoxDriver = new FirefoxDriver(service, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        public void InitChrome()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("incognito");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(path + @"\Drivers\", "chromedriver.exe");
                service.HideCommandPromptWindow = true;

                if (_chromeDriver == null)
                    _chromeDriver = new ChromeDriver(service, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void CloseChrome()
        {
            _chromeDriver?.Close();
        }

        public void CloseFirefox()
        {
            _firefoxDriver?.Close();
        }

        public void CloseEdge()
        {
            _edgeDriver?.Close();
        }

        public IWebDriver GetFirefox() => _firefoxDriver;

        public IWebDriver GetChrome() => _chromeDriver;

        public IWebDriver GetEdge() => _edgeDriver;
    }
}
