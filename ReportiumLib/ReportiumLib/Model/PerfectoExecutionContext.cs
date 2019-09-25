using System.Collections.Generic;
using System.Web.UI;
using OpenQA.Selenium;
using Reportium.Exceptions;

namespace Reportium.Model
{
    /// <summary>
    /// PerfectoExecutionContext
    /// The following properties are optional: Job, Project.
    /// </summary>
    public class PerfectoExecutionContext : BaseExecutionContext
    {

        public List<Pair> WebDriverPairs { get; set; }

        public PerfectoExecutionContext()
        {

            WebDriverPairs = new List<Pair>();
        }

        //protected PerfectoExecutionContext(PerfectoExecutionContextBuilder<PerfectoExecutionContext, PerfectoExecutionContextBuilder> builder) : base(builder)
        //{

        //    if (builder.webDriverPairs.Count == 0)
        //    {
        //        throw new ReportiumException("Missing required web driver(s) argument. Call your builder's withWebDriver() method");
        //    }

        //    WebDriverPairs.AddRange(builder.webDriverPairs);
        //}

        //public List<Pair> WebDriverPairs { get ; set; }

        public IWebDriver GetWebDriver()
        {
            return (IWebDriver)WebDriverPairs[0].Second;
        }

        public List<Pair> GetWebDriverPairs()
        {
            return WebDriverPairs;
        }

        public class PerfectoExecutionContextBuilder<T, P> : ExecutionContextBuilder<T, P>
        where T : PerfectoExecutionContextBuilder<T, P>
        where P : PerfectoExecutionContext, new()
           
        {

            private int index = 1;
            //internal List<Pair> webDriverPairs = new List<Pair>();

            public T WithWebDriver(IWebDriver webDriver)
            {
                return WithWebDriver(webDriver, index.ToString());
            }

            public T WithWebDriver(IWebDriver webDriver, string alias)
            {
                obj.WebDriverPairs.Add(new Pair(alias, webDriver));
                index++;
                return _this;
            }

            //public new PerfectoExecutionContext Build()
            //{
            //    return new PerfectoExecutionContext(this);
            //}
        }
    
    public class PerfectoExecutionContextBuilder : PerfectoExecutionContextBuilder<PerfectoExecutionContextBuilder, PerfectoExecutionContext> { }

    }
}