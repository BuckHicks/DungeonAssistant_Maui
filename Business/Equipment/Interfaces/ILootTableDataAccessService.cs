namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface ILootTableDataAccessService
{

    ILootTableModel GetLootTable(int id);
    ILootTableModel GetLootTable(string name);
    IList<ILootTableModel> GetLootTables();
    bool UpdateLootTable(ILootTableModel lootTable);
    bool UpdateLootTables(IList<ILootTableModel> lootTable);
    bool SaveLootTable(ILootTableModel lootTable);
    bool SaveLootTables(IList<ILootTableModel> lootTable);
    bool DeleteLootTable(ILootTableModel lootTable);
    bool DeleteLootTables(IList<ILootTableModel> lootTable);
}
