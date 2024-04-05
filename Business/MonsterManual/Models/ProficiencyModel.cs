using Newtonsoft.Json;

namespace DungeonAssistant.Business.MonsterManual.Models;

public class ProficiencyModel
{

    [JsonProperty("value")]
    public long Value { get; set; }

    [JsonProperty("proficiency")]
    public ProficiencyElementModel Element { get; set; }
}
