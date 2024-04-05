namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IMagicItemSearchAndFilterService
{
    IList<IMagicItemModel> Filter(IList<IMagicItemModel> list, string filter);
}
