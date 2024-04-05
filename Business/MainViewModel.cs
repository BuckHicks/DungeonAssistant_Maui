using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DungeonAssistant.Business.ActorCatalog.Interfaces;
using DungeonAssistant.Business.Commons.Interfaces;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.MonsterManual.Interfaces;
using System.Windows.Input;

namespace DungeonAssistant.Business;

public class MainViewModel : ObservableObject, IMainViewModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private ObservableObject _currentViewModel;
    private IMonsterManualViewModel _monsterManualViewModel;
    private IEquipmentViewModel _equipmentViewModel;
    private IMagicItemViewModel _magicItemViewModel;
    private ILootTableViewModel _lootTableViewModel;
    private IActorCatalogViewModel _actorCatalogViewModel;

    public MainViewModel(IMonsterManualViewModel monsterManualViewModel, IEquipmentViewModel equipmentViewModel, IMagicItemViewModel magicItemViewModel,
        ILootTableViewModel lootTableViewModel, IActorCatalogViewModel actorCatalogViewModel)
    {
        _monsterManualViewModel = monsterManualViewModel;
        _equipmentViewModel = equipmentViewModel;
        _magicItemViewModel = magicItemViewModel;
        _lootTableViewModel = lootTableViewModel;
        _actorCatalogViewModel = actorCatalogViewModel;

        MonsterManualCommand = new RelayCommand<ObservableObject>(x => ChangeView((ObservableObject)_monsterManualViewModel));
        EquipmentCommand = new RelayCommand<ObservableObject>(x => ChangeView((ObservableObject)_equipmentViewModel));
        MagicItemCommand = new RelayCommand<ObservableObject>(x => ChangeView((ObservableObject)_magicItemViewModel));
        LootCommand = new RelayCommand<ObservableObject>(x => ChangeView((ObservableObject)_lootTableViewModel));
        ActorCatalogCommand = new RelayCommand<ObservableObject>(x => ChangeView((ObservableObject)_actorCatalogViewModel));

        CurrentViewModel = (ObservableObject)_monsterManualViewModel;
    }

    private void ChangeView(ObservableObject newView)
    {
        CurrentViewModel = newView;
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\
    public ObservableObject CurrentViewModel
    {
        get { return _currentViewModel; }
        set
        {
            if (_currentViewModel != value)
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand MonsterManualCommand { get; set; }
    public ICommand EquipmentCommand { get; set; }
    public ICommand MagicItemCommand { get; set; }
    public ICommand LootCommand { get; set; }
    public ICommand ActorCatalogCommand { get; set; }
}
