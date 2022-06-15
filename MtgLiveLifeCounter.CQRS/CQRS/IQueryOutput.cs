using System.Net;
using System.Text.Json.Serialization;

namespace MtgLiveLifeCounter.Core
{
    public interface IQueryOutput
    {
        [JsonIgnore]
        HttpStatusCode HttpStatusCode { get; }

        [JsonIgnore]
        bool Success { get; }

        object Data { get; }

        string[] Messages { get; }
    }
}
