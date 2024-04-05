using Autofac;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;

namespace DungeonAssistant.Business.Equipment.Services;

public class EquipmentSlotFactoryService : IEquipmentSlotFactoryService
{
    private IEquipmentService _equipmentService;

    public EquipmentSlotFactoryService(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    public IEquipmentSlotModel GetEquipmentSlot()
    {
        return new EquipmentSlotModel(_equipmentService);
    }
}
