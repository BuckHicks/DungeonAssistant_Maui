namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface ILootTableModel 
{
    string Name { get; set; }
    int Id { get; set; }
    string Description { get; set; }
    string ImageSource { get; set; }
    int LootTableSize { get; set; }
    IList<IEquipmentSlotModel> EquipmentSlots { get; set; }
    bool IsDataComplete { get; set; }

}
