using DungeonAssistant.Business.MonsterManual.Interfaces;

namespace DungeonAssistant.Business.ActorCatalog.Interfaces;

public interface IActorModel
{
    string Name { get; set; }
    string Description { get; set; }
    IMonsterModel StatBlock { get; set; }
}
