namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface ILootTableViewModel
{
    ILootTableModel CurrentLootTable { get; set; }
    IList<ILootTableModel> LootTables { get; set; }
}
