using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.Equipment.Models;

public class RangeModel : IRangeModel
{
    [JsonProperty("normal")]
    public int? Normal { get; set; }

    [JsonProperty("long")]
    public int? Long { get; set; }
}
