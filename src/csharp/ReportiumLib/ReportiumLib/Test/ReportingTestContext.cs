using System.Collections.Generic;
using System.Collections.ObjectModel;
using Reportium.Model;
using Reportium.Model.Util;

namespace Reportium.Test
{
	/// <summary>
	/// Class denoting the test context, e.g. one or all of the following: test suite, CI build number, SCM branch name, ...
	/// </summary>
	public class ReportingTestContext
	{
		public ReportingTestContext(params string[] testExecutionTags)
		{
			if (testExecutionTags?.Length > 0)
			{
				List<string> tags = new List<string>();

				foreach (string tag in testExecutionTags)
				{
					if (!string.IsNullOrEmpty(tag))
					{
						tags.Add(tag);
					}
				}
				this.TestExecutionTags = tags;
			}
			else
			{
				this.TestExecutionTags = new List<string>();
			}
			this.CustomFields = new HashSet<CustomField>();
		}

		public ReportingTestContext(Builder builder)
		{
			TestExecutionTags = builder.TestExecutionTags;

			var fields = ExecutionContextPopulator
				.PopulateMissingCustomFieldsPropertiesFromEnvVariables(builder.CustomFields);
			CustomFields = new HashSet<CustomField>(fields);
		}

		public List<string> TestExecutionTags { get; }
		public HashSet<CustomField> CustomFields { get; }

		public override string ToString()
		{
			return new ToStringBuilder<ReportingTestContext>(this)
				.Append(x => x.TestExecutionTags)
				.Append(x => x.CustomFields)
				.ToString();
		}

		public class Builder
		{
			public Builder()
			{
				TestExecutionTags = new List<string>();
				CustomFields = new HashSet<CustomField>();
			}

			public Builder(ReportingTestContext copy)
			{
				TestExecutionTags = copy.TestExecutionTags;
				CustomFields = copy.CustomFields;
			}

			public List<string> TestExecutionTags { get; }

			public HashSet<CustomField> CustomFields { get; }

			public Builder WithTestExecutionTags(Collection<string> testExecutionTags)
			{
				if (testExecutionTags?.Count > 0)
				{
					foreach (string testExecutionTag in testExecutionTags)
					{
						if (!string.IsNullOrEmpty(testExecutionTag))
						{
							this.TestExecutionTags.Add(testExecutionTag);
						}
					}
				}
				return this;
			}

			public Builder WithTestExecutionTags(params string[] testExecutionTags)
			{
				if (testExecutionTags != null)
				{
					WithTestExecutionTags(new Collection<string>(testExecutionTags));
				}
				return this;
			}

			public Builder WithCustomFields(HashSet<CustomField> customFields)
			{
				if (customFields?.Count > 0)
				{
					foreach (CustomField customField in customFields)
					{
						if (customField != null)
						{
							this.CustomFields.Add(customField);
						}
					}
				}
				return this;
			}

			public Builder WithCustomFields(params CustomField[] customFields)
			{
				if (customFields != null)
				{
					WithCustomFields(new HashSet<CustomField>(customFields));
				}
				return this;
			}

			public ReportingTestContext Build()
			{
				return new ReportingTestContext(this);
			}
		}
	}
}