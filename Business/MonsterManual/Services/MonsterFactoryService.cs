using Autofac;
using DungeonAssistant.Business.MonsterManual.Interfaces;

namespace DungeonAssistant.Business.MonsterManual.Services;

public class MonsterFactoryService : IMonsterFactoryService
{
    //private ILifetimeScope _scope;
    //private ISpeedFactoryService _speedFactory;

    //public MonsterFactoryService(ISpeedFactoryService speedFactory)
    //{
    //    //_scope = scope;
    //    _speedFactory = speedFactory;
    //}

    //public IMonsterModel GetMonster()
    //{
    //    return (Application.Current as App)?.Host?.Services.GetRequiredService<IMonsterModel>();
    //    //return _scope.Resolve<IMonsterModel>();
    //}

    //TODO: Implement this method
    public IMonsterModel GetMonster()
    {
        throw new NotImplementedException();
    }
}
