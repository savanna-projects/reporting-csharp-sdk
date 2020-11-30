namespace Reportium.Test.Result
{
	/// <summary>
	/// Interface test result
	/// </summary>
	/// <remarks>This interface represent Reportium test result object.</remarks>
	public interface ITestResult
	{
		/// <summary>
		/// Represent test result success = true , failure = false
		/// </summary>
		/// <returns>true if test success otherwise returns false</returns>
		bool IsSuccessful { get; }

		/// <summary>
		/// Returns attached message to the test result.
		/// </summary>
		/// <returns>string message</returns>
		string Message { get; }

		/// <summary>
		/// Returns attached failure reason to the test result.
		/// </summary>
		/// <returns>string message</returns>
		string FailureReasonName { get; }
	}
}