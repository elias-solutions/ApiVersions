using System.Text.Json.Serialization;

namespace Api.Models.V2
{
    public class SuperSample
    {
        public SuperSample(Status status)
        {
            Status = status;
        }

        [JsonPropertyName("status")] public Status Status { get; }
    }
}