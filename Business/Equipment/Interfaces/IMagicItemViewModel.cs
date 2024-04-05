namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IMagicItemViewModel
{
    IMagicItemModel CurrentMagicItem { get; set; }
    IList<IMagicItemModel> MagicItems { get; set; }
}
