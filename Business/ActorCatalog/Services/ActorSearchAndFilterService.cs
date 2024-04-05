
using DungeonAssistant.Business.ActorCatalog.Interfaces;

namespace DungeonAssistant.Business.ActorCatalog.Services;

public class ActorSearchAndFilterService : IActorSearchAndFilterService
{
    public IList<IActorModel> Filter(IList<IActorModel> list, string filter)
    {
        IList<IActorModel> results = new List<IActorModel>();
        foreach (IActorModel actor in list)
        {
            string buffer = actor.Name.ToLower();
            if (buffer.Contains(filter.ToLower()))
            {
                results.Add(actor);
            }
        }

        return results;
    }
}
