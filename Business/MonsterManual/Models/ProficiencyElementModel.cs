using Newtonsoft.Json;

namespace DungeonAssistant.Business.MonsterManual.Models;

public class ProficiencyElementModel
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
