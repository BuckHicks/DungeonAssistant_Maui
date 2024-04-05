
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;

namespace DungeonAssistant.Business.Equipment.Services;

public class EquipmentService : IEquipmentService
{
    private bool _isDebugOn = false;
    private IEquipmentFactoryService _equipmentFactoryService;
    private IEquipmentDataAccessService _equipmentDataAccessService;
    private IEquipmentSearchAndFilterService _equipmentSearchAndFilter;
    private IList<IEquipmentModel> _equipment;
    private IList<IEquipmentModel> _equipmentRaw;

    public EquipmentService (IEquipmentFactoryService equipmentFactoryService, IEquipmentDataAccessService equipmentDataAccessService, IEquipmentSearchAndFilterService equipmentSearchAndFilter)
    {
        _equipmentFactoryService = equipmentFactoryService;
        _equipmentDataAccessService = equipmentDataAccessService;
        _equipmentSearchAndFilter = equipmentSearchAndFilter;

        Equipment = GetAllEquipment();
    }

    public async Task<IList<IEquipmentModel>> GetAllEquipmentAsync()
    {
        return (await _equipmentDataAccessService.GetAllEquipmentAsync())
            .Cast<IEquipmentModel>()
            .ToList() as IList<IEquipmentModel>;
    }

    public IList<IEquipmentModel> GetAllEquipment()
    {
        _equipmentRaw = Equipment = _equipmentDataAccessService.GetAllEquipment();
        return Equipment;
    }

    public async Task<IEquipmentModel> GetEquipmentAsync(string name)
    {
        return await _equipmentDataAccessService.GetEquipmentAsync(name);
    }

    public IEquipmentModel GetEquipment()
    {
        return _equipmentFactoryService.GetEquipment();
    }

    public bool SaveEquipment(IEquipmentModel equipmentModel)
    {
        return _equipmentDataAccessService.SaveEquipment(equipmentModel);
    }

    public int AddEquipment()
    {
        IList<IEquipmentModel> equipment = new List<IEquipmentModel>();
        foreach (IEquipmentModel model in Equipment)
        {
            equipment.Add(model);
        }

        IEquipmentModel newEquipment = _equipmentFactoryService.GetEquipment();
        newEquipment.Name = "{{Name}}";
        newEquipment.Description = new List<DescriptionModel> { new DescriptionModel("{{Description}}") };
        
        equipment.Add(newEquipment);

        IEnumerable<IEquipmentModel> sortedEquipment = equipment.OrderBy(x => x.Name);
        equipment = sortedEquipment.ToList();
        _equipmentRaw = Equipment = equipment;
        return equipment.IndexOf(newEquipment);
    }

    public IList<IEquipmentModel> Filter(string filter)
    {
        return _equipmentSearchAndFilter.Filter(_equipmentRaw, filter);
    }

    public IList<IEquipmentModel> Equipment
    {
        get { return _equipment; }
        set
        {
            if (_equipment != value)
            {
                _equipment = value;
            }
        }
    }
}
