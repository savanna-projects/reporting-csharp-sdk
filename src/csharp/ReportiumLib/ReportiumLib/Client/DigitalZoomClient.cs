using Reportium.Test.Result;
using Reportium.Test;

using System;

namespace Reportium.Client
{
    /// <summary>
    /// Reportium client interface
    /// </summary>
    /// <remarks> All reporting clients implements this interface </remarks>
    public interface IDigitalZoomClient
    {
        /// <summary>
        /// Create a new test execution
        /// </summary>
        /// <param name="name">Test name</param>
        /// <param name="context">Testing environment context, e.g. CI build number</param>
        void TestStart(string name, ReportingTestContext context);

        /// <summary>
        /// Indicates that the test has stopped and its execution status.
        /// </summary>
        /// <param name="testResult">Test execution result</param>
        void TestStop(ITestResult testResult);

        /// <summary>
        /// Indicates that the test has stopped and its execution status.
        /// </summary>
        /// <param name="testResult">Test execution result</param>
        /// <param name="testContext">Testing environment context, e.g. CI build number</param>
        void TestStop(ITestResult testResult, ReportingTestContext testContext);

        /// <summary>
        /// Log a new logical step for the current test, e.g. "Submit shopping cart"
        /// </summary>
        /// <param name="description">Step description</param>
        [Obsolete("This method is deprecated, Please use StepStart instead.", false)]
        void TestStep(string description);

        /// <summary>
        /// Log the beginning of a new logical step for the current test, e.g. "Submit shopping cart"
        /// </summary>
        /// <param name="description">Step description</param>
        void StepStart(string description);

        /// <summary>
        /// Log the end of the previous test step
        /// </summary>
        /// <param name="message">End message</param>
        void StepEnd(string message = null);

        /// <summary>
        /// Returns the URL to the created on-line report in Perfecto's reporting solution.
        /// </summary>
        /// <remarks>The report is based on all tests that match the current execution context, and is not limited to a single functional test execution.</remarks>
        /// <returns> URL to the created on-line report </returns>
        string GetReportUrl();
    }
}