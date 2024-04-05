
using DungeonAssistant.Business.Equipment.Interfaces;

namespace DungeonAssistant.Business.Equipment.Services;

public class LootTableSearchAndFilterService : ILootTableSearchAndFilterService
{
    public IList<ILootTableModel> Filter(IList<ILootTableModel> list, string filter)
    {
        IList<ILootTableModel> results = new List<ILootTableModel>();
        foreach (ILootTableModel lootTable in list)
        {
            string buffer = lootTable.Name.ToLower();
            if (buffer.Contains(filter.ToLower()))
            {
                results.Add(lootTable);
            }
        }

        return results;
    }
}
