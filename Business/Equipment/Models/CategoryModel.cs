using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.Equipment.Models;

public class CategoryModel : ICategoryModel
{
    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}
