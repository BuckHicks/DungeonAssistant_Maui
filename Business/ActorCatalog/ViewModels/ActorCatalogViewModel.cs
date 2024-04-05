using CommunityToolkit.Mvvm.ComponentModel;
using DungeonAssistant.Business.ActorCatalog.Interfaces;

namespace DungeonAssistant.Business.ActorCatalog.ViewModels;

public partial class ActorCatalogViewModel : ObservableObject, IActorCatalogViewModel
{
    [ObservableProperty] private IActorModel currentActor;
    [ObservableProperty] private IList<IActorModel> actors;
    [ObservableProperty] private int selectedActorIndex = -1;

    private void GetActorDetails()
    {
        throw new NotImplementedException();
    }
}
