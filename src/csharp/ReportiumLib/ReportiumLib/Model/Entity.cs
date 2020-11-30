using System.Runtime.Serialization;

namespace Reportium.model
{
    [DataContract]
    public class Entity
    {
        public Entity(string id)
        {
            Id = id;
        }

        [DataMember]
        public string Id { get; set; }
    }
}