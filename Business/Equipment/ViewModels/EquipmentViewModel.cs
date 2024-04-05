using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.ViewModels;

public class EquipmentViewModel : ObservableObject, IEquipmentViewModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private bool _isDebugOn = true;

    private IEquipmentService _equipmentService;
    //private IEquipmentFactoryService _equipmentFactory;
    //private IEquipmentDataAccessService _equipmentDataAccessService;
    //private IEquipmentSearchAndFilterService _equipmentSearchAndFilter;
    private IEquipmentModel _currentEquipment;
    private IList<IEquipmentModel> _equipment;
    private IList<IEquipmentModel> _equipmentRaw;
    private string _filter = "";
    private int _selectedEquipmentIndex = -1;
    private bool _isEditEnabled;
    private string _editIconSource;
    private StrongReferenceMessenger messenger;

    private const string UNLOCKED_IMAGE_PATH = "Assets/Images/UnlockIcon.png";
    private const string LOCKED_IMAGE_PATH = "Assets/Images/LockIcon.png";

    public EquipmentViewModel(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        //_equipmentFactory = equipmentFactory;
        //_equipmentDataAccessService = equipmentDataAccessObject;
        //_equipmentSearchAndFilter = equipmentSearchAndFilter;

        SaveCommand = new RelayCommand(SaveEquipment);

        WeakReferenceMessenger.Default.Register<EquipmentViewModel, MessageWindowResponse, string>(this, "GetEquipmentDetails", (recipient, message) =>
        {
            if (message.Response)
            {
                GetEquipmentDetailsAsync();
            }
        });

        GetEquipment();
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\
    private async void GetEquipmentAsync()
    {
        _equipmentRaw = Equipment = (await _equipmentService.GetAllEquipmentAsync())
            .Cast<IEquipmentModel>()
            .ToList() as IList<IEquipmentModel>;

        ToggleEditCommand = new RelayCommand(ToggleEdit);
        AddEquipmentCommand = new RelayCommand(AddEquipment);
        EditIconSource = LOCKED_IMAGE_PATH;
    }

    private void GetEquipment()
    {
        _equipmentRaw = Equipment = _equipmentService.Equipment;

        
        ToggleEditCommand = new RelayCommand(ToggleEdit);
        AddEquipmentCommand = new RelayCommand(AddEquipment);
        EditIconSource = LOCKED_IMAGE_PATH;
    }

    private async void GetEquipmentDetailsAsync()
    {
        CurrentEquipment = Equipment[SelectedEquipmentIndex];
        if (CurrentEquipment.IsDataComplete == false)
        {
            Equipment[SelectedEquipmentIndex] = (await _equipmentService.GetEquipmentAsync(Equipment[SelectedEquipmentIndex].Name)) as IEquipmentModel;

            // The monster api failed and returned null
            if (Equipment[SelectedEquipmentIndex] == null)
            {
                Equipment[SelectedEquipmentIndex] = CurrentEquipment;
                WeakReferenceMessenger.Default.Send(new MessageWindowConfiguration
                {
                    Message = "An error occurred while getting " + CurrentEquipment.Name + " data. Would you like to try again? " +
                    "Check you internet connection if you continue to see this message.",
                    IsOkVisible = false,
                    IsTrueFalseVisible = true,
                    Token = "GetEquipmentDetails"
                });
            }
            else
            {
                Equipment[SelectedEquipmentIndex].IsDataComplete = true;
                CurrentEquipment = Equipment[SelectedEquipmentIndex];
            }
        }
    }

    private void SaveEquipment()
    {
        _equipmentService.SaveEquipment(CurrentEquipment);
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

    private void AddEquipment()
    {
        int selectedIndex = _equipmentService.AddEquipment();
        _equipmentRaw = Equipment = _equipmentService.Equipment;
        SelectedEquipmentIndex = selectedIndex;
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\
    public int SelectedEquipmentIndex
    {
        get { return _selectedEquipmentIndex; }
        set
        {
            if (_selectedEquipmentIndex != value)
            {
                _selectedEquipmentIndex = value;
                if (_selectedEquipmentIndex > -1)
                {
                    CurrentEquipment = Equipment[_selectedEquipmentIndex];
                }
                OnPropertyChanged();
            }
        }
    }

    public IEquipmentModel CurrentEquipment
    {
        get { return _currentEquipment; }
        set
        {
            if (_currentEquipment != value)
            {
                _currentEquipment = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<IEquipmentModel> Equipment
    {
        get { return _equipment; }
        set
        {
            if (_equipment != value)
            {
                _equipment = value;
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
                Equipment = _equipmentService.Filter(_filter);
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

    public ICommand AddEquipmentCommand { get; set; }

    public ICommand SaveCommand { get; set; }
}
