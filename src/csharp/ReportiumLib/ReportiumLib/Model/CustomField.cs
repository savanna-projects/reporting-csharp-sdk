using Reportium.Model.Util;

using System.Runtime.Serialization;

namespace Reportium.Model
{
	[DataContract]
	public class CustomField
	{
		public CustomField() { }

		public CustomField(string name, string value)
		{
			Name = name;
			Value = value;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Value { get; set; }

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}

			if (!(obj is CustomField))
			{
				return false;
			}

			var that = (CustomField)obj;
			if (!Name.Equals(that.Name))
			{
				return false;
			}

			return Value != null ? Value.Equals(that.Value) : that.Value == null;
		}

		public override int GetHashCode()
		{
			// setup
			int result = Name.GetHashCode();

			// get
			return (31 * result) + ((Value?.GetHashCode()) ?? 0);
		}

		public override string ToString()
		{
			return new ToStringBuilder<CustomField>(this)
				.Append(x => x.Name)
				.Append(x => x.Value)
				.ToString();
		}
	}
}