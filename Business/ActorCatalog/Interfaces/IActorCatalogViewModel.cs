namespace DungeonAssistant.Business.ActorCatalog.Interfaces;

public interface IActorCatalogViewModel
{
    IActorModel CurrentActor { get; set; }
    IList<IActorModel> Actors { get; set; }
}
