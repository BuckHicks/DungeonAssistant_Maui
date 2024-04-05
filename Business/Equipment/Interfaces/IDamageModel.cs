namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IDamageModel
{
    string DamageDice { get; set; }

    ICategoryModel DamageType { get; set; }
}
