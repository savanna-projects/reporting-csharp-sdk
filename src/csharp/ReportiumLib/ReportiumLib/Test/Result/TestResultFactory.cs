using System;

namespace Reportium.Test.Result
{
	public static class TestResultFactory
	{
		/// <summary>
		/// create successful test result object
		/// </summary>
		/// <returns>TestResultSuccess object</returns>
		public static ITestResult CreateSuccess()
		{
			return new TestResultSuccess();
		}

		/// <summary>
		/// Creates a failed test execution result
		/// </summary>
		/// <param name="message">Test failure message </param>
		/// <returns>TestResultFailure object</returns>
		public static ITestResult CreateFailure(string message)
		{
			return new TestResultFailure(message, null, null);
		}

		/// <summary>
		/// Creates a failed test execution result
		/// </summary>
		/// <param name="message">Test failure message</param>
		/// <param name="ex">exception thrown caused failure</param>
		/// <returns>TestResultFailure object</returns>
		public static ITestResult CreateFailure(string message, Exception ex)
		{
			return new TestResultFailure(message, ex, null);
		}

		/// <summary>
		/// Creates a failed test execution result
		/// </summary>
		/// <param name="ex">exception thrown caused failure</param>
		/// <returns>TestResultFailure object</returns>
		public static ITestResult CreateFailure(Exception ex)
		{
			return CreateFailure(null, ex, null);
		}

		/// <summary>
		/// Creates a failed test execution result
		/// </summary>
		/// <param name="ex">exception thrown caused failure</param>
		///  <param name="failureReason">test failure reason</param>
		/// <returns>TestResultFailure object</returns>
		public static ITestResult CreateFailure(Exception ex, string failureReason)
		{
			return CreateFailure(null, ex, failureReason);
		}

		/// <summary>
		/// Creates a failed test execution result
		/// </summary>
		/// <param name="message">Test failure message</param>
		/// <param name="ex">exception thrown caused failure</param>
		///  <param name="failureReason">test failure reason</param>
		/// <returns>TestResultFailure object</returns>
		public static ITestResult CreateFailure(string message, Exception ex, string failureReason)
		{
			return new TestResultFailure(message, ex, failureReason);
		}
	}
}