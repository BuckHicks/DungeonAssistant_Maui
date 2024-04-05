namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IEquipmentSearchAndFilterService
{
    IList<IEquipmentModel> Filter(IList<IEquipmentModel> list, string filter);
}
