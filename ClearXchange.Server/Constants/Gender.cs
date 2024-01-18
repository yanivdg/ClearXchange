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
        Male,

        [Description("Female")]
        [EnumMember(Value = "Female")]
        Female,

        //[Description("Other")]
        [EnumMember(Value = "Other")]
        Other
    }
}
