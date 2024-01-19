using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ClearXchange.Server.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {

        [Description("Male")]
        [EnumMember(Value = "Male")]
        Male = 1,

        [Description("Female")]
        [EnumMember(Value = "Female")]
        Female =2,

        [Description("Other")]
        [EnumMember(Value = "Other")]
        Other = 3
    }
}
