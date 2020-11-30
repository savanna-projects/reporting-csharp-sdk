
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Reportium.Model
{
    /// <summary>
    /// PerfectoExecutionContext The following properties are optional: Job, Project.
    /// </summary>
    public class PerfectoExecutionContext : BaseExecutionContext
    {
        public PerfectoExecutionContext()
        {
            WebDriverPairs = new List<KeyValuePair<string, IWebDriver>>();
        }

        public List<KeyValuePair<string, IWebDriver>> WebDriverPairs { get; set; }

        public IWebDriver GetWebDriver()
        {
            return WebDriverPairs[0].Value;
        }

        public List<KeyValuePair<string, IWebDriver>> GetWebDriverPairs()
        {
            return WebDriverPairs;
        }

        public class PerfectoExecutionContextBuilder<T, P> : ExecutionContextBuilder<T, P>
            where T : PerfectoExecutionContextBuilder<T, P>
            where P : PerfectoExecutionContext, new()
        {
            private int index = 1;

            public T WithWebDriver(IWebDriver webDriver)
            {
                return WithWebDriver(webDriver, index.ToString());
            }

            public T WithWebDriver(IWebDriver webDriver, string alias)
            {
                obj.WebDriverPairs.Add(new KeyValuePair<string, IWebDriver>(alias, webDriver));
                index++;
                return _this;
            }
        }

        public class PerfectoExecutionContextBuilder : PerfectoExecutionContextBuilder<PerfectoExecutionContextBuilder, PerfectoExecutionContext>
        { }
    }
}