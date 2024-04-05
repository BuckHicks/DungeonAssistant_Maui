using System;
using System.Linq;
using DungeonAssistant.Business.Equipment.Models;

namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IEquipmentDataAccessService
{
    Task<List<EquipmentModel>> GetAllEquipmentAsync();
    Task<EquipmentModel> GetEquipmentAsync(string name);
    IEquipmentModel GetEquipment(int id);
    IEquipmentModel GetEquipment(string name);
    IList<IEquipmentModel> GetAllEquipment();
    bool SaveEquipment(IEquipmentModel equipment);
    bool SaveEquipment(IList<IEquipmentModel> equipment);
    bool DeleteEquipment(IEquipmentModel equipment);
    bool DeleteEquipment(IList<IEquipmentModel> equipment);
}
