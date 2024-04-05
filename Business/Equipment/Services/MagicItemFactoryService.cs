using Autofac;
using DungeonAssistant.Business.Equipment.Interfaces;

namespace DungeonAssistant.Business.Equipment.Services;

public class MagicItemFactoryService : IMagicItemFactoryService
{

    public IMagicItemModel GetMagicItem()
    {
        return (Application.Current as App)?.Host?.Services.GetRequiredService<IMagicItemModel>();
    }
}
