using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.Models;

public class EquipmentModel : ObservableObject, IEquipmentModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\

    private int _id;
    private bool _isDataComplete;
    private string _name;
    private double _weight;
    private IList<DescriptionModel> _description;
    private IList<string> _stringdescription;
    private ICostModel _cost;
    private IDamageModel _twoHandedDamage;
    private IList<EquipmentPropertyModel> _properties;
    private IRangeModel _range;
    private IDamageModel _damage;
    private string _categoryRange;
    private string _weaponRange;
    private string _weaponCategory;
    private string _toolCategory;
    private string _vehicleCategory;
    private ICategoryModel _gearCategory;
    private ICategoryModel _equipmentCategory;
    private bool _isStealthDisadvantage;
    private int _strengthRequirement;
    private IArmorClassModel _armorClass;
    private string _armorCategory;
    private int _packageId;
    private List<string> _equipmentCategories = new List<string> 
    {
        "Adventuring Gear",
        "Tools",
        "Weapon",
        "Armor",
        "Mounts and Vehicles"
    };
    private string _selectedEquipmentCategory;


    private string _armorIconPath = "Assets/Images/ArmorIcon.png";
    private string _arrowsIconPath = "Assets/Images/ArrowsIcon.png";
    private string _battleAxeIconPath = "Assets/Images/BattleAxeIcon.png";
    private string _bowIconPath = "Assets/Images/BowIcon.png";
    private string _crossbowIconPath = "Assets/Images/CrossbowIcon.png";
    private string _daggerIconPath = "Assets/Images/DaggerIcon.png";
    private string _hatchetIconPath = "Assets/Images/HatchetIcon.png";
    private string _potionIconPath = "Assets/Images/PotionIcon.png";
    private string _shieldIconPath = "Assets/Images/ShieldIcon.png";
    private string _spearIconPath = "Assets/Images/SpearIcon.png";
    private string _swordIconPath = "Assets/Images/SwordIcon.png";
    private string _wandIconPath = "Assets/Images/WandIcon.png";
    private string _warhammerIconPath = "Assets/Images/WarhammerIcon.png";

    public EquipmentModel()
    {
        Name = "Name";

        Description = new List<DescriptionModel>();
        TwoHandedDamage = new DamageModel();
        Properties = new List<EquipmentPropertyModel>();
        Range = new RangeModel();
        Damage = new DamageModel();
        ArmorClass = new ArmorClassModel();
        EquipmentCategory = new CategoryModel();
        GearCategory = new CategoryModel();
        Cost = new CostModel();

        Description.Add(new DescriptionModel("Description"));

        AddDescriptionCommand = new RelayCommand(AddDescription);
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\

    private void AddDescription()
    {
        IList<DescriptionModel> descriptions = new List<DescriptionModel>();
        foreach (DescriptionModel d in Description)
        {
            descriptions.Add(d);
        }

        descriptions.Add(new DescriptionModel("{{New Line}}"));
        Description = descriptions;
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\

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

    [JsonProperty("name")]
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("equipment_category")]
    public ICategoryModel EquipmentCategory
    {
        get { return _equipmentCategory; }
        set
        {
            if (_equipmentCategory != value)
            {
                _equipmentCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("gear_category")]
    public ICategoryModel GearCategory
    {
        get { return _gearCategory; }
        set
        {
            if (_gearCategory != value)
            {
                _gearCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("vehicle_category")]
    public string VehicleCategory
    {
        get { return _vehicleCategory; }
        set
        {
            if (_vehicleCategory != value)
            {
                _vehicleCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("tool_category")]
    public string ToolCategory
    {
        get { return _toolCategory; }
        set
        {
            if (_toolCategory != value)
            {
                _toolCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("weapon_category")]
    public string WeaponCategory
    {
        get { return _weaponCategory; }
        set
        {
            if (_weaponCategory != value)
            {
                _weaponCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("weapon_range")]
    public string WeaponRange
    {
        get { return _weaponRange; }
        set
        {
            if (_weaponRange != value)
            {
                _weaponRange = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("category_range")]
    public string CategoryRange
    {
        get { return _categoryRange; }
        set
        {
            if (_categoryRange != value)
            {
                _categoryRange = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("damage")]
    public IDamageModel Damage
    {
        get { return _damage; }
        set
        {
            if (_damage != value)
            {
                _damage = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("range")]
    public IRangeModel Range
    {
        get { return _range; }
        set
        {
            if (_range != value)
            {
                _range = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("properties")]
    public IList<EquipmentPropertyModel> Properties
    {
        get { return _properties; }
        set
        {
            if (_properties != value)
            {
                _properties = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("two_handed_damage")]
    public IDamageModel TwoHandedDamage
    {
        get { return _twoHandedDamage; }
        set
        {
            if (_twoHandedDamage != value)
            {
                _twoHandedDamage = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("armor_category")]
    public string ArmorCategory
    {
        get { return _armorCategory; }
        set
        {
            if (_armorCategory != value)
            {
                _armorCategory = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("armor_class")]
    public IArmorClassModel ArmorClass
    {
        get { return _armorClass; }
        set
        {
            if (_armorClass != value)
            {
                _armorClass = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("str_minimum")]
    public int StrengthRequirement
    {
        get { return _strengthRequirement; }
        set
        {
            if (_strengthRequirement != value)
            {
                _strengthRequirement = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("stealth_disadvantage")]
    public bool IsStealthDisadvantage
    {
        get { return _isStealthDisadvantage; }
        set
        {
            if (_isStealthDisadvantage != value)
            {
                _isStealthDisadvantage = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("cost")]
    public ICostModel Cost
    {
        get { return _cost; }
        set
        {
            if (_cost != value)
            {
                _cost = value;
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("weight")]
    public double Weight
    {
        get { return _weight; }
        set
        {
            if (_weight != value)
            {
                _weight = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<DescriptionModel> Description
    {
        get { return _description; }
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    // This implementation is messy. I want to clean this up in the near future. 
    // Cleanup should be done when the source of the data is changed and data type is corrected
    [JsonProperty("desc")]
    public IList<string> StringDescription
    {
        get { return _stringdescription; }
        set
        {
            if (_stringdescription != value)
            {
                _stringdescription = value;
                List<DescriptionModel> buffer = new List<DescriptionModel>();
                foreach(string s in _stringdescription)
                {
                    buffer.Add(new DescriptionModel(s));
                }
                Description = buffer;
                OnPropertyChanged();
            }
        }
    }

    public string ImageSource
    {
        get 
        {
            if (EquipmentCategory.Name == "Armor" && ArmorCategory == "Shield")
            {
                return _shieldIconPath;
            }
            else if (EquipmentCategory.Name == "Armor")
            {
                return _armorIconPath;
            }
            else if (EquipmentCategory.Name == "Weapon" && WeaponRange == "Ranged")
            {
                return _bowIconPath;
            }
            else if (EquipmentCategory.Name == "Weapon")
            {
                return _swordIconPath;
            }
            else if(EquipmentCategory.Name == "Adventuring Gear" && GearCategory.Name == "Ammunition")
            {
                return _arrowsIconPath;
            }
            else
            {
                return _potionIconPath;
            }
        }
    }

    public ICommand AddDescriptionCommand { get; set; }

    public bool IsDataComplete
    {
        get { return _isDataComplete; }
        set { _isDataComplete = value; }
    }

    public int PackageId
    {
        get { return _packageId; }
        set
        {
            if (_packageId != value)
            {
                _packageId = value;
                OnPropertyChanged();
            }
        }
    }

    public List<string> EquipmentCategories
    {
        get { return _equipmentCategories; }
        set
        {
            if(_equipmentCategories != value)
            {
                _equipmentCategories = value;
                OnPropertyChanged();
            }
        }
    }

    public string SelectedEquipmentCategory
    {
        get { return _selectedEquipmentCategory; }
        set
        {
            if(_selectedEquipmentCategory != value)
            {
                _selectedEquipmentCategory = value;
                OnPropertyChanged();
            }
        }
    }
}
