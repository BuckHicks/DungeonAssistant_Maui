namespace DungeonAssistant.Business.MonsterManual.Interfaces;

public interface IMonsterSearchAndFilterService
{
    IList<IMonsterModel> Filter(IList<IMonsterModel> list, string filter);
}
