using Autofac;
using DungeonAssistant.Business.ActorCatalog.Interfaces;

namespace DungeonAssistant.Business.ActorCatalog.Services;

public class ActorFactoryService : IActorFactoryService
{
    private ILifetimeScope _scope;

    public ActorFactoryService(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public IActorModel GetActor()
    {
        return _scope.Resolve<IActorModel>();
    }
}
