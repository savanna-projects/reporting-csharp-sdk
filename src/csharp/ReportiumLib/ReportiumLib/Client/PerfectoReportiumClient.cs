using OpenQA.Selenium;

using Reportium.Exceptions;
using Reportium.Model;
using Reportium.Test;
using Reportium.Test.Result;

using System;
using System.Collections.Generic;

namespace Reportium.Client
{
    /// <summary>
    /// Perfecto Reportium Client
    /// </summary>
    /// <remarks>Implements ReportiumClient interface, this client supports connection via the MCM</remarks>
    public class PerfectoReportiumClient : IReportiumClient
    {
        // constants: commands
        private const string StartTestCommand = "mobile:test:start";
        private const string StartStepCommand = "mobile:step:start";
        private const string EndStepCommand = "mobile:step:end";
        private const string EndTestCommand = "mobile:test:end";
        private const string AssertCommand = "mobile:status:assert";

        // constants: parameters
        private const string CustomFieldsParamName = "customFields";
        private const string TagsParamName = "tags";
        private const string FailureReasonParamName = "failureReason";
        private const string FailureDescriptionParamName = "failureDescription";
        private const string SuccessParamName = "success";

        // constants: symbols
        private const string Equal = "=";

        // constants: deprecated
        private const string TEST_STEP_COMMAND = "mobile:test:step";

        // members
        private readonly PerfectoExecutionContext context;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"> Test execution context details </param>
        public PerfectoReportiumClient(PerfectoExecutionContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Starting a new test
        /// </summary>
        /// <param name="name">The test name</param>
        /// <param name="context">Context tags to be attached to this particular test</param>
        public void TestStart(string name, ReportingTestContext context)
        {
            // setup
            var param = new Dictionary<string, object>();
            var job = this.context.Job;

            // build
            if (job != null)
            {
                param["jobName"] = job.Name;
                param["jobNumber"] = job.Number;
                param["jobBranch"] = job.Branch;
            }

            var project = this.context.Project;
            if (project != null)
            {
                param["projectName"] = project.Name;
                param["projectVersion"] = project.Version;
            }
            param["name"] = name;

            // setup
            var tags = new List<string>(this.context.ContextTags);
            tags.AddRange(context.TestExecutionTags);

            // put
            param[TagsParamName] = tags;

            // setup
            var customFieldsPair = new List<string>();
            var existingCustomFields = new HashSet<string>();

            // build
            CreateFieldsValuePairs(context.CustomFields, existingCustomFields, customFieldsPair);
            CreateFieldsValuePairs(this.context.CustomFields, existingCustomFields, customFieldsPair);

            // put
            param[CustomFieldsParamName] = customFieldsPair;

            // execute command
            ExecuteScript(StartTestCommand, param);
        }

        /// <summary>
        /// Logical test step
        /// </summary>
        /// <param name="description">Step description</param>
        [Obsolete("This method is deprecated, Please use stepStart instead.", false)]
        public void TestStep(string description)
        {
            // setup
            var param = new Dictionary<string, object>
            {
                ["name"] = description
            };

            // execute command
            ExecuteScript(TEST_STEP_COMMAND, param);
        }

        /// <summary>
        /// Log the beginning of a new logical step for the current test, e.g. "Submit shopping cart"
        /// </summary>
        /// <param name="description">Step description</param>
        public void StepStart(string description)
        {
            // setup
            var param = new Dictionary<string, object>
            {
                ["name"] = description
            };

            // execute command
            ExecuteScript(StartStepCommand, param);
        }

        /// <summary>
        /// Log the end of the previous test step
        /// </summary>
        /// <param name="message">Description Step description</param>
        public void StepEnd(string message = null)
        {
            // setup
            var param = new Dictionary<string, object>
            {
                ["message"] = message
            };

            // execute command
            ExecuteScript(EndStepCommand, param);
        }

        /// <summary>
        /// Indicates that the test has stopped and its execution status.
        /// </summary>
        /// <param name="testResult">Test execution result</param>
        public void TestStop(ITestResult testResult)
        {
            DoTestStop(testResult, new ReportingTestContext());
        }

        /// <summary>
        /// Indicates that the test has stopped and its execution status.
        /// </summary>
        /// <param name="testResult">Test execution result</param>
        /// <param name="testContext">Testing environment context, e.g. CI build number</param>
        public void TestStop(ITestResult testResult, ReportingTestContext testContext)
        {
            DoTestStop(testResult, testContext);
        }

        private void DoTestStop(ITestResult testResult, ReportingTestContext testContext)
        {
            // setup
            var isSuccessful = testResult.IsSuccessful;
            var param = new Dictionary<string, object>
            {
                [SuccessParamName] = isSuccessful
            };

            // on failure
            if (!isSuccessful)
            {
                param[FailureDescriptionParamName] = testResult.Message;

                var failureReasonName = testResult.FailureReasonName;
                if (!string.IsNullOrEmpty(failureReasonName))
                {
                    param[FailureReasonParamName] = failureReasonName;
                }
            }

            // on null
            if (testContext == null)
            {
                ExecuteScript(EndTestCommand, param);
                return;
            }

            // when not null
            var testExecutionTags = testContext.TestExecutionTags;
            if (testExecutionTags?.Count > 0)
            {
                param[TagsParamName] = new List<string>(testExecutionTags);
            }

            var customFields = testContext.CustomFields;
            if (customFields?.Count > 0)
            {
                var customFieldsPair = new List<string>();
                CreateFieldsValuePairs(customFields, new HashSet<string>(), customFieldsPair);
                param[CustomFieldsParamName] = customFieldsPair;
            }

            // execute command
            ExecuteScript(EndTestCommand, param);
        }

        /// <summary>
        /// Get the report
        /// </summary>
        /// <returns>String representation of the report URL</returns>
        public string GetReportUrl()
        {
            // setup
            var driver = context.GetWebDriver();

            // exit conditions
            if (driver is not IHasCapabilities)
            {
                throw new ReportiumException("WebDriver instance is assumed to have Selenium Capabilities");
            }

            // extract capability
            var capability = ((IHasCapabilities)driver).Capabilities.GetCapability(Constants.Capabilities.ExecutionReportUrl);

            // get
            return capability == null ? null : $"{capability}";
        }

        /// <summary>
        /// Script execution
        /// </summary>
        /// <param name="script">Script to execute</param>
        /// <param name="param">Parameters</param>
        private void ExecuteScript(string script, IDictionary<string, object> param)
        {
            // setup
            var webdriver = context.GetWebDriver();

            // execute script
            ((IJavaScriptExecutor)webdriver).ExecuteScript(script, param);
        }

        /// <summary>
        /// Adding assertions to the Execution Report. This method will not fail the test
        /// </summary>
        /// <param name="message">Used to label the assertion</param>
        /// <param name="status">Indicates the result of the verification operation</param>
        public void ReportiumAssert(string message, bool status)
        {
            // setup
            var param = new Dictionary<string, object>
            {
                ["message"] = message,
                ["status"] = status
            };

            // execute script
            ExecuteScript(AssertCommand, param);
        }

        private static void CreateFieldsValuePairs(
            IEnumerable<CustomField> populatedFields,
            ICollection<string> fieldsNames,
            ICollection<string> pairs)
        {
            // exit conditions
            if (pairs == null)
            {
                return;
            }

            // setup
            if (fieldsNames == null)
            {
                fieldsNames = new HashSet<string>();
            }

            foreach (var customField in populatedFields)
            {
                var value = string.IsNullOrEmpty(customField.Value) ? string.Empty : customField.Value;
                var name = customField.Name;

                if (string.IsNullOrEmpty(name))
                {
                    throw new ReportiumException("Custom field name cannot be empty");
                }

                if (!fieldsNames.Contains(name))
                {
                    pairs.Add(name + Equal + value);
                }
                fieldsNames.Add(name);
            }
        }
    }
}