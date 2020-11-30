using System.Runtime.Serialization;

namespace Reportium.Model
{
	[DataContract]
	public class Job
	{
		public Job() { }

		public Job(string name, int number)
		{
			Name = name;
			Number = number;
		}

		[DataMember]
		public int Number { get; set; }  // build number 

		[DataMember]
		public string Name { get; set; } // might be used as branch name

		[DataMember]
		public string Branch { get; set; }

		public Job WithBranch(string branch)
		{
			Branch = branch;
			return this;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Job job = (Job)obj;
			return Number == job.Number && Name == job.Name && Branch == job.Branch;
		}

		public override int GetHashCode()
		{
			// build
			int result = Name.GetHashCode();

			// calculate
			result = (31 * result) + Number.GetHashCode();
			result = (31 * result) + Branch.GetHashCode();

			// get
			return result;
		}
	}
}