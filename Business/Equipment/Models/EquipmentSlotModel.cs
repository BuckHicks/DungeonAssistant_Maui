using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.Models;

public class EquipmentSlotModel : ObservableObject, IEquipmentSlotModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\

    private int _id;
    private int _lootTableId;
    private IEquipmentModel _equipment;
    private IList<IEquipmentModel> _filteredEquipmentList;
    private IList<IEquipmentModel> _equipmentRaw;
    private IEquipmentService _equipmentService;
    private string _name;
    private int _multiplier;
    private int _index;
    private int _selectedEquipmentIndex;

    public EquipmentSlotModel(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        _equipmentRaw = new List<IEquipmentModel>()
        {
            new EquipmentModel()
            {
                Name = "Axe"
            },
            new EquipmentModel()
            {
                Name = "Arrow"
            },
            new EquipmentModel()
            {
                Name = "Battle Axe"
            },
            new EquipmentModel()
            {
                Name = "Katana"
            },
            new EquipmentModel()
            {
                Name = "Broad Sword"
            }
        };
        FilteredEquipmentList = new List<IEquipmentModel>();

        AddDescriptionCommand = new RelayCommand(AddDescription);
        RemoveCommand = new RelayCommand(Remove);
        Equipment = new EquipmentModel();
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\

    private void AddDescription()
    {
        IList<DescriptionModel> descriptions = new List<DescriptionModel>();
        foreach (DescriptionModel d in Equipment.Description)
        {
            descriptions.Add(d);
        }

        descriptions.Add(new DescriptionModel("{{New Line}}"));
        Equipment.Description = descriptions;
    }

    private void Remove()
    {
        
        WeakReferenceMessenger.Default.Send(this);
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\


    public string Name
    {
        get { return _name; }
        set 
        { 
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
                if (_name.Length == 0)
                {
                    FilteredEquipmentList = new List<IEquipmentModel>();
                }
                else
                {
                    FilteredEquipmentList = _equipmentService.Filter(_name);
                }
            }
        }
    }

    public int SelectedEquipmentIndex 
    {
        get { return _selectedEquipmentIndex; }
        set
        {
            if (_selectedEquipmentIndex != value)
            {
                _selectedEquipmentIndex = value;
                if(_selectedEquipmentIndex > -1)
                {
                    Equipment = FilteredEquipmentList[_selectedEquipmentIndex];
                    FilteredEquipmentList = new List<IEquipmentModel>();
                }
                OnPropertyChanged();
            }
        }
    }


    public IEquipmentModel Equipment
    {
        get { return _equipment; }
        set
        {
            if (_equipment != value)
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }
    }

    public int Multiplier
    {
        get { return _multiplier; }
        set
        {
            if (_multiplier != value)
            {
                _multiplier = value;
                OnPropertyChanged();
            }
        }
    }

    public int Index
    {
        get { return _index; }
        set
        {
            if(_index != value)
            {
                _index = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<IEquipmentModel> FilteredEquipmentList
    {
        get { return _filteredEquipmentList; }
        set
        {
            if (_filteredEquipmentList != value)
            {
                _filteredEquipmentList = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand AddDescriptionCommand { get; set; }

    public ICommand RemoveCommand { get; set; }

    public int Id
    {
        get { return _id; }
        set
        {
            if (_id != value)
            {
                _id = value;
                OnPropertyChanged();
            }
        }
    }

    public int LootTableId
    {
        get { return _lootTableId; }
        set
        {
            if (_lootTableId != value)
            {
                _lootTableId = value;
                OnPropertyChanged();
            }
        }
    }
}
