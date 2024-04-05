using CommunityToolkit.Mvvm.ComponentModel;
using DungeonAssistant.Business.ActorCatalog.Interfaces;
using DungeonAssistant.Business.MonsterManual.Interfaces;


namespace DungeonAssistant.Business.ActorCatalog.Models;

public class ActorModel : ObservableObject, IActorModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IMonsterModel StatBlock { get; set; }
}
