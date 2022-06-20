using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Helpers
{
    public static class WebDriverExtensions
    {
        public static bool FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                try
                {
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    return wait.Until(drv => driver.IsElementVisible(by));
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        private static bool IsElementVisible(this IWebDriver driver, By searchElementBy)
        {
            try
            {
                return driver.FindElement(searchElementBy).Displayed;

            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
