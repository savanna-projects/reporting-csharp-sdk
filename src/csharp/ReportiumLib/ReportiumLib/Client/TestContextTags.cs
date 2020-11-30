using System.Collections.Generic;

namespace Reportium.test
{
    /// <summary>
    /// Class denoting the test context
    /// </summary>
    /// <remarks>e.g. one or all of the following: test suite, CI build number, SCM branch name, ...</remarks>
    public class TestContextTags
    {
        /// <summary>
        /// Creates a TestContextTags object
        /// </summary>
        /// <param name="testExecutionTags"> One or more strings represents a test tags. </param>
        public TestContextTags(params string[] testExecutionTags)
        {
            TestExecutionTags = new List<string>(testExecutionTags);
        }

        public IList<string> TestExecutionTags { get; }
    }
}