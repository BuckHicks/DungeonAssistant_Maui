using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.ViewModels;

public class MagicItemViewModel : ObservableObject, IMagicItemViewModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private bool _isDebugOn = false;

    private IMagicItemFactoryService _magicItemFactory;
    private IMagicItemDataAccessService _magicItemDataAccessObject;
    private IMagicItemSearchAndFilterService _magicItemSearchAndFilter;
    private IMagicItemModel _CurrentMagicItem;
    private IList<IMagicItemModel> _magicItems;
    private IList<IMagicItemModel> _magicItemsRaw;
    private string _filter = "";
    private int _SelectedMagicItemIndex = -1;
    private bool _isEditEnabled;
    private string _editIconSource;

    private const string UNLOCKED_IMAGE_PATH = "Assets/Images/UnlockIcon.png";
    private const string LOCKED_IMAGE_PATH = "Assets/Images/LockIcon.png";

    public MagicItemViewModel(IMagicItemFactoryService magicItemFactory, IMagicItemDataAccessService magicItemDataAccessObject, IMagicItemSearchAndFilterService magicItemSearchAndFilter)
    {
        _magicItemFactory = magicItemFactory;
        _magicItemDataAccessObject = magicItemDataAccessObject;
        //_magicItemSearchAndFilter = magicItemSearchAndFilter;

        EditIconSource = LOCKED_IMAGE_PATH;

        
        ToggleEditCommand = new RelayCommand(ToggleEdit);

        WeakReferenceMessenger.Default.Register<MagicItemViewModel, MessageWindowResponse, string>(this, "GetMagicItemDetails", (recipient, message) =>
        {
            if (message.Response)
            {
                GetMagicItemDetails();
            }
        });

        //GetMagicItems();
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\
    private async void GetMagicItems()
    {
        _magicItemsRaw = MagicItems = (await _magicItemDataAccessObject.GetAllMagicItems())
            .Cast<IMagicItemModel>()
            .ToList() as IList<IMagicItemModel>;

        if (_isDebugOn)
        {
            foreach (IMagicItemModel magicItem in MagicItems)
            {
                Console.WriteLine(magicItem.Name);
            }
        }
    }

    private async void GetMagicItemDetails()
    {
        CurrentMagicItem = MagicItems[SelectedMagicItemIndex];
        if (CurrentMagicItem.IsDataComplete == false)
        {
            MagicItems[SelectedMagicItemIndex] = (await _magicItemDataAccessObject.GetMagicItem(MagicItems[SelectedMagicItemIndex].Name)) as IMagicItemModel;

            // The monster api failed and returned null
            if (MagicItems[SelectedMagicItemIndex] == null)
            {
                MagicItems[SelectedMagicItemIndex] = CurrentMagicItem;

                WeakReferenceMessenger.Default.Send(new MessageWindowConfiguration
                {
                    Message = "An error occurred while getting " + CurrentMagicItem.Name + " data. Would you like to try again? " +
                    "Check you internet connection if you continue to see this message.",
                    IsOkVisible = false,
                    IsTrueFalseVisible = true,
                    Token = "GetMagicItemDetails"
                });
            }
            else
            {
                MagicItems[SelectedMagicItemIndex].IsDataComplete = true;
                CurrentMagicItem = MagicItems[SelectedMagicItemIndex];
            }
        }

        if (_isDebugOn)
        {
            Console.Write(CurrentMagicItem.EquipmentCategory.Name);
        }
    }

    private void ToggleEdit()
    {
        if (_isEditEnabled)
        {
            IsEditEnabled = false;
        }
        else
        {
            IsEditEnabled = true;
        }
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\
    public int SelectedMagicItemIndex
    {
        get { return _SelectedMagicItemIndex; }
        set
        {
            if (_SelectedMagicItemIndex != value)
            {
                _SelectedMagicItemIndex = value;
                if (_SelectedMagicItemIndex > -1)
                {
                    GetMagicItemDetails();
                }
                OnPropertyChanged();
            }
        }
    }

    public IMagicItemModel CurrentMagicItem
    {
        get { return _CurrentMagicItem; }
        set
        {
            if (_CurrentMagicItem != value)
            {
                _CurrentMagicItem = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<IMagicItemModel> MagicItems
    {
        get { return _magicItems; }
        set
        {
            if (_magicItems != value)
            {
                _magicItems = value;
                OnPropertyChanged();
            }
        }
    }

    public string Filter
    {
        get { return _filter; }
        set
        {
            if (_filter != value)
            {
                _filter = value;
                MagicItems = _magicItemSearchAndFilter.Filter(_magicItemsRaw, _filter);
                OnPropertyChanged();
            }
        }
    }

    public bool IsEditEnabled
    {
        get { return _isEditEnabled; }
        set
        {
            if (_isEditEnabled != value)
            {
                _isEditEnabled = value;
                if (_isEditEnabled)
                {
                    EditIconSource = UNLOCKED_IMAGE_PATH;
                }
                else
                {
                    EditIconSource = LOCKED_IMAGE_PATH;
                }
                OnPropertyChanged();
            }
        }
    }

    public string EditIconSource
    {
        get { return _editIconSource; }
        set
        {
            if (_editIconSource != value)
            {
                _editIconSource = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ToggleEditCommand { get; set; }
}
