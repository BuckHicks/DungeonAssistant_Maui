using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.Equipment.Models;

public class DamageModel : IDamageModel
{
    public DamageModel()
    {
        DamageType = new CategoryModel();
    }

    [JsonProperty("damage_dice")]
    public string DamageDice { get; set; }

    [JsonProperty("damage_type")]
    public ICategoryModel DamageType { get; set; }
}
