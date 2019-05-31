using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestApiClient.Configuration
{
    class JsonSerializerConfiguration : JsonSerializerSettings
    {
        internal JsonSerializerConfiguration()
        {
            Formatting = Formatting.Indented;
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}