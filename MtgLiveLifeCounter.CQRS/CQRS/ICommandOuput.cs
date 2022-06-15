using System.Net;
using System.Text.Json.Serialization;

namespace MtgLiveLifeCounter.Core
{

    public interface ICommandOuput
    {
        [JsonIgnore]
        HttpStatusCode HttpStatusCode { get; }

        [JsonIgnore]
        string Uri { get; }

        [JsonIgnore]
        bool Success { get; }

        string[] Messages { get; }
    }

    public interface ICommandOuput<T> : ICommandOuput
    {
        T Data { get; }
    }



}
