using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DungeonAssistant.Business.MonsterManual.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.MonsterManual.Models;

public partial class MonsterModel : ObservableObject, IMonsterModel
{
    private int _charisma;
    private int _constitution;
    private int _dexterity;
 
    [ObservableProperty]
    private List<int> _hitDice = new List<int>
    {
        4, 6, 8, 10, 12, 20
    };

    private List<string> _hitDiceImageSources = new List<string>
    {
        "Assets/Images/d04Icon.png",
        "Assets/Images/d06Icon.png",
        "Assets/Images/d08Icon.png",
        "Assets/Images/d10Icon.png",
        "Assets/Images/d12Icon.png",
        "Assets/Images/d20Icon.png",
    };
    private int _hitDiceQuantity;
    private int _hitDie;
    [ObservableProperty] private string hitDieImageSource = "Assets/Images/d20Icon.png";
    private int _hitPointBase;

    private string _hitPointsCalculation;
    private int _intelligence;
    [ObservableProperty] private int _intelligenceModifier;
    private bool _isDataComplete;
    [JsonProperty("subtype")][ObservableProperty] private string _monsterSubtype;
    private int _strength;
    private int _wisdom;
    [ObservableProperty] private string alignment;
    [JsonProperty("armor_class")][ObservableProperty] private List<ArmorClassModel> armorClass;
    [JsonProperty("challenge_rating")][ObservableProperty] private double challengeRating;
    [ObservableProperty] private int charismaModifier;
    [ObservableProperty] private int constitutionModifier;
    [ObservableProperty]
    private List<string> creatureAlignments = new List<string>
    {
        "lawful good", "neutral good", "chaotic good",
        "lawful neutral", "neutral", "chaotic neutral",
        "lawful evil", "neutral evil", "chaotic evil",
        "any lawful alignment", "any chaotic alignment",
        "any good alignment", "any evil alignment", "any neutral alignment",
        "any non-lawful alignment", "any non-chaotic alignment",
        "any non-good alignment", "any non-evil alignment", "any non-neutral alignment",
        "any alignment", "unaligned",
    };

    [ObservableProperty]
    private List<string> creatureSizes = new List<string>
    {
        "Tiny", "Small", "Medium", "Large", "Huge", "Gargantuan"
    };

    [ObservableProperty] private int dexterityModifier;
    [JsonProperty("hit_points")][ObservableProperty] private int hitPoints;
    [JsonProperty("type")][ObservableProperty] private string monsterType;
    [ObservableProperty] private string name;
    [ObservableProperty] private List<ProficiencyModel> proficiencies;
    [ObservableProperty] private string size;
    [ObservableProperty] private SpeedModel speed;
    [ObservableProperty] private int strengthModifier;
    [ObservableProperty] private int wisdomModifier;
    [JsonProperty("xp")][ObservableProperty] private int xp;

    public MonsterModel()
    {
        Speed = new SpeedModel();
        ArmorClass = new List<ArmorClassModel>();
    }

    public int Charisma
    {
        get { return _charisma; }
        set
        {
            if (_charisma != value)
            {
                _charisma = value;
                CharismaModifier = CalculateModifier(_charisma);
                OnPropertyChanged();
            }
        }
    }


    public int Constitution
    {
        get { return _constitution; }
        set
        {
            if (_constitution != value)
            {
                _constitution = value;
                ConstitutionModifier = CalculateModifier(_constitution);
                OnPropertyChanged();
            }
        }
    }




    public int Dexterity
    {
        get { return _dexterity; }
        set
        {
            if (_dexterity != value)
            {
                _dexterity = value;
                DexterityModifier = CalculateModifier(_dexterity);
                OnPropertyChanged();
            }
        }
    }



    public int HitDiceQuantity
    {
        get { return _hitDiceQuantity; }
        set
        {
            if (_hitDiceQuantity != value)
            {
                _hitDiceQuantity = value;
                UpdateHitPointCalculation();
                OnPropertyChanged();
            }
        }
    }

    public int HitDie
    {
        get { return _hitDie; }
        set
        {
            if (_hitDie != value)
            {
                _hitDie = value;
                switch (_hitDie)
                {
                    case 4:
                        HitDieImageSource = _hitDiceImageSources[0];
                        break;
                    case 6:
                        HitDieImageSource = _hitDiceImageSources[1];
                        break;
                    case 8:
                        HitDieImageSource = _hitDiceImageSources[2];
                        break;
                    case 10:
                        HitDieImageSource = _hitDiceImageSources[3];
                        break;
                    case 12:
                        HitDieImageSource = _hitDiceImageSources[4];
                        break;
                    default:
                        HitDieImageSource = _hitDiceImageSources[5];
                        break;
                }
                UpdateHitPointCalculation();
                OnPropertyChanged();
            }
        }
    }

    public int HitPointBase
    {
        get { return _hitPointBase; }
        set
        {
            if (_hitPointBase != value)
            {
                _hitPointBase = value;
                UpdateHitPointCalculation();
                OnPropertyChanged();
            }
        }
    }

    [JsonProperty("hit_dice")]
    public string HitPointsCalculation
    {
        get { return _hitPointsCalculation; }
        set
        {
            if (_hitPointsCalculation != value)
            {
                _hitPointsCalculation = value;
                ExtractHitPointValues();
                OnPropertyChanged();
            }
        }
    }

    public Guid Id { get; set; }

    public int Intelligence
    {
        get { return _intelligence; }
        set
        {
            if (_intelligence != value)
            {
                _intelligence = value;
                IntelligenceModifier = CalculateModifier(_intelligence);
                OnPropertyChanged();
            }
        }
    }

    public bool IsDataComplete
    {
        get { return _isDataComplete; }
        set { _isDataComplete = value; }
    }

    public int Strength
    {
        get { return _strength; }
        set
        {
            if (_strength != value)
            {
                _strength = value;
                StrengthModifier = CalculateModifier(_strength);
                OnPropertyChanged();
            }
        }
    }

    public int Wisdom
    {
        get { return _wisdom; }
        set
        {
            if (_wisdom != value)
            {
                _wisdom = value;
                WisdomModifier = CalculateModifier(_wisdom);
                OnPropertyChanged();
            }
        }
    }

    [RelayCommand]
    private void Add(string ability)
    {
        switch (ability)
        {
            case "STR":
                Strength++;
                break;
            case "DEX":
                Dexterity++;
                break;
            case "CON":
                Constitution++;
                break;
            case "INT":
                Intelligence++;
                break;
            case "WIS":
                Wisdom++;
                break;
            case "CHA":
                Charisma++;
                break;
        }
    }

    [RelayCommand]
    private void AddArmorClass()
    {
        List<ArmorClassModel> armorClasses = new List<ArmorClassModel>();
        foreach (ArmorClassModel d in ArmorClass)
        {
            armorClasses.Add(d);
        }

        armorClasses.Add(new ArmorClassModel());
        ArmorClass = armorClasses;
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\

    private int CalculateModifier(int score)
    {
        int modifier = (score - 10) / 2;
        if (score < 10 && score % 2 == 1)
        {
            modifier--;
        }
        return modifier;
    }

    private void ExtractHitPointValues()
    {
        if (HitPointsCalculation != null && HitPointsCalculation != string.Empty)
        {
            int length = HitPointsCalculation.Length;
            int dIndex = HitPointsCalculation.IndexOf("d");
            int pIndex = HitPointsCalculation.IndexOf("+");
            int eIndex = pIndex > 0 ? pIndex : length;

            if (dIndex > 0)
            {
                HitDiceQuantity = Convert.ToInt32(HitPointsCalculation.Substring(0, dIndex));
                HitDie = Convert.ToInt32(HitPointsCalculation.Substring(dIndex + 1, eIndex - dIndex - 1));
            }
            else
            {
                HitDiceQuantity = 0;
                HitDie = HitDice[5];
            }

            if (pIndex > 0)
            {
                HitPointBase = Convert.ToInt32(HitPointsCalculation.Substring(pIndex + 1, length - pIndex - 1));
            }
            else
            {
                HitPointBase = 0;
            }
        }
    }

    [RelayCommand]
    private void Subtract(string ability)
    {
        switch (ability)
        {
            case "STR":
                Strength--;
                break;
            case "DEX":
                Dexterity--;
                break;
            case "CON":
                Constitution--;
                break;
            case "INT":
                Intelligence--;
                break;
            case "WIS":
                Wisdom--;
                break;
            case "CHA":
                Charisma--;
                break;
        }
    }

    private void UpdateHitPointCalculation()
    {
        if (HitPointBase > 0 && HitDiceQuantity > 0 && HitDie > 0)
        {
            HitPointsCalculation = HitDiceQuantity + "d" + HitDie + "+" + HitPointBase;
        }
        else if (HitDiceQuantity > 0 && HitDie > 0)
        {
            HitPointsCalculation = HitDiceQuantity + "d" + HitDie;
        }

        if (HitDiceQuantity > 0 && HitDie > 0)
        {
            HitPoints = HitPointBase + HitDiceQuantity * ((HitDie / 2) + 1);
        }
    }
}
