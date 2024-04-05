using DungeonAssistant.Business.Equipment.Models;

namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IMagicItemDataAccessService
{
    Task<List<MagicItemModel>> GetAllMagicItems();
    Task<MagicItemModel> GetMagicItem(string name);
}
