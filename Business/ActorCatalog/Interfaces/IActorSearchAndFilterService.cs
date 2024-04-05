namespace DungeonAssistant.Business.ActorCatalog.Interfaces;

public interface IActorSearchAndFilterService
{
    IList<IActorModel> Filter(IList<IActorModel> list, string filter);
}
