using DungeonAssistant.Business.Commons.Models;

namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IMagicItemModel
{
    Guid Id { get; set; }

    string Name { get; set; }

    IList<DescriptionModel> Description { get; set; }

    ICategoryModel EquipmentCategory { get; set; }

    bool IsDataComplete { get; set; }
}
