namespace DungeonAssistant.Business.Equipment.Interfaces;

public interface IEquipmentService
{
    Task<IList<IEquipmentModel>> GetAllEquipmentAsync();
    IList<IEquipmentModel> GetAllEquipment();
    Task<IEquipmentModel> GetEquipmentAsync(string name);
    IEquipmentModel GetEquipment();
    bool SaveEquipment(IEquipmentModel equipmentModel);
    IList<IEquipmentModel> Filter(string filter);
    int AddEquipment();
    IList<IEquipmentModel> Equipment { get; set; }
}
