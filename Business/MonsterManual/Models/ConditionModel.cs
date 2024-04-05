using CommunityToolkit.Mvvm.ComponentModel;
using DungeonAssistant.Business.Commons.Models;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.MonsterManual.Models;
public class ConditionModel : ObservableObject
{
    private string _name;
    private List<DescriptionModel> _description;

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    [JsonProperty("desc")]
    public List<DescriptionModel> Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

}
