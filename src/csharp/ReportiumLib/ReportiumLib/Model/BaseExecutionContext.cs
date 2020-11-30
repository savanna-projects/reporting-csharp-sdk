using System;
using System.Collections.Generic;
using System.Linq;

namespace Reportium.Model
{
	/// <summary>
	/// PerfectoExecutionContext. The following properties are optional: Job, Project.
	/// </summary>
	public class BaseExecutionContext
	{
        public BaseExecutionContext()
        {
            ContextTags = new List<string>();
            CustomFields = new HashSet<CustomField>();
        }

        public Job Job { get; set; }
        public Project Project { get; set; }
        public List<string> ContextTags { get; set; }
        public HashSet<CustomField> CustomFields { get; set; }

        public class BuilderBase<T, P>
            where T : BuilderBase<T, P>
            where P : class, new()
        {
            protected P obj;
            protected T _this;

            protected BuilderBase()
            {
                obj = new P();
                _this = (T)this;
            }

            public P Build()
            {
                P result = obj;
                obj = null;
                return result;
            }
        }

        /// <summary>
        /// ExecutionContextBuilder
        /// </summary>
        /// <remarks>Build a PerfectoExecutionContext object using given configuration</remarks>
        public class ExecutionContextBuilder<T, P> : BuilderBase<T, P>
            where T : ExecutionContextBuilder<T, P>
            where P : BaseExecutionContext, new()
        {
            public T WithJob(Job job)
			{
                obj.Job = job;
				return _this;
			}

            public T WithProject(Project project)
			{
                // setup
                obj.Project = project;

                // get
                return _this;
			}

            public T WithContextTags(string[] contextTags)
            {
                // setup
                obj.ContextTags = contextTags.Length > 0 ? contextTags.ToList() : new List<string>();

                // get
                return _this;
            }

			public T WithCustomFields(ICollection<CustomField> customFields)
			{
                // setup
                customFields ??= Array.Empty<CustomField>();

                // exit conditions
                if (customFields.Count == 0)
				{
                    return _this;
				}

                // get
                foreach (CustomField customField in customFields)
                {
                    if (customField != null)
                    {
                        obj.CustomFields.Add(customField);
                    }
                }
                return _this;
			}
        }

        public class ExecutionContextBuilder : ExecutionContextBuilder<ExecutionContextBuilder, BaseExecutionContext>
        { }
    }
}
