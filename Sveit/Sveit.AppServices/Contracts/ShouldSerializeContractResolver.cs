using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sveit.Models;
using System.Reflection;

namespace Sveit.AppServices.Contracts
{
    /// <summary>
    /// Permite a customização em como o JsonSerializer serializa e desserializa objetos em json.
    /// </summary>
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public new static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(Player) && property.PropertyName == "Password")
            {
                property.ShouldSerialize = instance => { return false; };
            }
            if (property.DeclaringType == typeof(Player) && property.PropertyName == "Email")
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }
}