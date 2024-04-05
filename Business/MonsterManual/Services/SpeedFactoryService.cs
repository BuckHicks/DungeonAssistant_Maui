using Autofac;
using DungeonAssistant.Business.MonsterManual.Interfaces;

namespace DungeonAssistant.Business.MonsterManual.Services;

public class SpeedFactoryService : ISpeedFactoryService
{
    //private ILifetimeScope _scope;

    public SpeedFactoryService()
    {
        //_scope = scope;
    }

    public ISpeedModel GetSpeed()
    {
        throw new NotImplementedException();
    }

    //public ISpeedModel GetSpeed()
    //{
    //    //return (Application.Current as App)?.Host?.Services.GetRequiredService<ISpeedModel>();
    //    //return _scope.Resolve<ISpeedModel>();
    //}
}
