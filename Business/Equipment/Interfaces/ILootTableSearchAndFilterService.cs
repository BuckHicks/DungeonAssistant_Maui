namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface ILootTableSearchAndFilterService
{
    IList<ILootTableModel> Filter(IList<ILootTableModel> list, string filter);
}
