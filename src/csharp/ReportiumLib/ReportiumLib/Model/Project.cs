using System.Runtime.Serialization;

namespace Reportium.Model
{
    /// <summary>
    /// Project which contains multiple tests
    /// </summary>
    [DataContract]
    public class Project
    {
        public Project()
        {
        }

        public Project(string name, string version)
        {
            Name = name;
            Version = version;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Version { get; set; }

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

            var project = (Project)obj;
            return Name == project.Name && Version == project.Version;
        }

        public override int GetHashCode()
        {
            // setup
            var result = Name.GetHashCode();

            // get
            return (31 * result) + Version.GetHashCode();
        }
    }
}