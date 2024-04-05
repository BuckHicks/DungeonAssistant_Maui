using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonAssistant.Business.MonsterManual.Models;

public class ArmorClassModel : ObservableObject
{
    private string _armorType;
    private int _value;
    private ConditionModel _condition;

    [JsonProperty("type")]
    public string ArmorType
    {
        get { return _armorType; }
        set
        {
            _armorType = value;
            OnPropertyChanged();
        }
    }

    [JsonProperty("value")]
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }

    [JsonProperty("condition")]
    public ConditionModel Condition
    {
        get { return _condition; }
        set
        {
            _condition = value;
            OnPropertyChanged();
        }
    }

}
