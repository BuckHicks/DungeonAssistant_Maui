using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.Equipment.Models;

public class EquipmentPropertyModel : IEquipmentPropertyModel
{
    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("desc")]
    public string[] Desc { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}
