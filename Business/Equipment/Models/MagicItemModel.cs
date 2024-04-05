using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using Newtonsoft.Json;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.Models;

public partial class MagicItemModel : ObservableObject, IMagicItemModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private bool _isDataComplete;
    private IList<string> _stringDescription;

    [ObservableProperty] 
    private IList<DescriptionModel> description;

    [JsonProperty("equipment_category"),ObservableProperty] 
    private ICategoryModel equipmentCategory;
    
    [JsonProperty("name")][ObservableProperty] 
    private string name;


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

    public MagicItemModel()
    {
        Description = new List<DescriptionModel>();
        EquipmentCategory = new CategoryModel();
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\

    [RelayCommand]
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

    public Guid Id { get; set; }

    // This implementation is messy. I want to clean this up in the near future. 
    [JsonProperty("desc")]
    public IList<string> StringDescription
    {
        get { return _stringDescription; }
        set
        {
            if (_stringDescription != value)
            {
                _stringDescription = value;
                List<DescriptionModel> buffer = new List<DescriptionModel>();
                foreach (string s in _stringDescription)
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
            if (EquipmentCategory.Name == "Armor")
            {
                return _shieldIconPath;
            }
            else if (EquipmentCategory.Name == "Weapon")
            {
                return _swordIconPath;
            }
            else
            {
                return _potionIconPath;
            }
        }
    }

    public bool IsDataComplete
    {
        get { return _isDataComplete; }
        set { _isDataComplete = value; }
    }
}
