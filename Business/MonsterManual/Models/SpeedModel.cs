using CommunityToolkit.Mvvm.ComponentModel;
using DungeonAssistant.Business.MonsterManual.Interfaces;
using Newtonsoft.Json;

namespace DungeonAssistant.Business.MonsterManual.Models;

public partial class SpeedModel : ObservableObject, ISpeedModel
{
    [JsonProperty("walk")]
    [ObservableProperty] private string walk;
    [JsonProperty("fly")]
    [ObservableProperty] private string fly;
    [JsonProperty("swim")]
    [ObservableProperty] private string swim;
    [JsonProperty("burrow")]
    [ObservableProperty] private string burrow;
    [JsonProperty("climb")]
    [ObservableProperty] private string climb;
    [JsonProperty("hover")]
    [ObservableProperty] private bool hover;
}
