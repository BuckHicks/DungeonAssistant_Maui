using Autofac;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;

namespace DungeonAssistant.Business.Equipment.Services;

public class EquipmentFactoryService : IEquipmentFactoryService
{

    public IEquipmentModel GetEquipment()
    {
        return (Application.Current as App)?.Host?.Services.GetRequiredService<IEquipmentModel>();
    }
}
