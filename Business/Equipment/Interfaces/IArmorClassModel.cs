namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IArmorClassModel
{
    int? Base { get; set; }

    bool DexBonus { get; set; }

    int? MaxBonus { get; set; }
}
