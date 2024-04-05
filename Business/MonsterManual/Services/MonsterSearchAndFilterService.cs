

using DungeonAssistant.Business.MonsterManual.Interfaces;

namespace DungeonAssistant.Business.MonsterManual.Services;

public class MonsterSearchAndFilterService : IMonsterSearchAndFilterService
{
    public IList<IMonsterModel> Filter(IList<IMonsterModel> list, string filter)
    {
        IList<IMonsterModel> results = new List<IMonsterModel>();
        foreach (IMonsterModel monster in list)
        {
            string buffer = monster.Name.ToLower();
            if (buffer.Contains(filter.ToLower()))
            {
                results.Add(monster);
            }
        }

        return results;

    }
}
