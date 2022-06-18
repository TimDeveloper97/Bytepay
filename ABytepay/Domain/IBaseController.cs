using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Domain
{
    public interface IBaseController
    {
        void InitEdge();
        void InitFirefox();
        void InitChrome();
        void CloseChrome();
        void CloseFirefox();
        void CloseEdge();
        IWebDriver GetFirefox();
        IWebDriver GetChrome();
        IWebDriver GetEdge();
    }
}
