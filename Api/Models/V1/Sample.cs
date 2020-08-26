using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models.V1
{
    public class Sample
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}