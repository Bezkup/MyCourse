
namespace src.Models.Options

{
  
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CoursesOptions
    {
        public long PerPage { get; set; }

        public CoursesOrderOptions Order { get; set; }
    }

    public partial class CoursesOrderOptions
    {
    
        public string By { get; set; }
    
        public bool Ascending { get; set; }

        public string[] Allow { get; set; }
    }

    public partial class CoursesOptions
    {
        public static CoursesOptions FromJson(string json) => JsonConvert.DeserializeObject<CoursesOptions>(json, src.Models.Options.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CoursesOptions self) => JsonConvert.SerializeObject(self, src.Models.Options.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
