using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;
using System.Windows.Input;

namespace DungeonAssistant.Business.Equipment.ViewModels;


public class LootTableViewModel : ObservableObject, ILootTableViewModel
{
    private bool _isDebugOn = false;

    private ILootTableFactoryService _lootTableFactoryService;
    private IEquipmentSlotFactoryService _equipmentSlotFactoryService;
    private ILootTableDataAccessService _lootTableDataAccessService;
    private ILootTableSearchAndFilterService _lootTableSearchAndFilter;
    private IEquipmentService _equipmentService;
    private ILootTableModel _currentLootTable;
    private IList<ILootTableModel> _lootTables;
    private IList<ILootTableModel> _lootTablesRaw;
    private int _selectedLootTableIndex = -1;
    private string _filter = "";
    private bool _isEditEnabled;
    private string _editIconSource;

    private const string UNLOCKED_IMAGE_PATH = "Assets/Images/UnlockIcon.png";
    private const string LOCKED_IMAGE_PATH = "Assets/Images/LockIcon.png";

    public LootTableViewModel(ILootTableFactoryService lootTableFactory, IEquipmentSlotFactoryService equipmentSlotFactoryService, IEquipmentService equipmentService,
        ILootTableDataAccessService lootTableDataAccessService, ILootTableSearchAndFilterService lootTableSearchAndFilter)
    {
        _lootTableFactoryService = lootTableFactory;
        _equipmentSlotFactoryService = equipmentSlotFactoryService;
        _equipmentService = equipmentService;
        _lootTableDataAccessService = lootTableDataAccessService;
        _lootTableSearchAndFilter = lootTableSearchAndFilter;


        ToggleEditCommand = new RelayCommand(ToggleEdit);
        AddLootSlotCommand = new RelayCommand(AddLootSlot);
        AddLootTableCommand = new RelayCommand(AddLootTable);
        EditIconSource = LOCKED_IMAGE_PATH;
        

        WeakReferenceMessenger.Default.Register<LootTableViewModel, MessageWindowResponse, string>(this, "GetLootTableDetails", (recipient, message) =>
        {
            if (message.Response)
            {
                GetLootTableDetails();
            }
        });

        //GetLootTables();
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\
    private void GetLootTables()
    {
        _lootTablesRaw = LootTables = _lootTableDataAccessService.GetLootTables();
    }

    private void GetLootTableDetails() 
    {
        CurrentLootTable = LootTables[SelectedLootTableIndex];
        if (CurrentLootTable.IsDataComplete == false)
        {
            LootTables[SelectedLootTableIndex] = (_lootTableDataAccessService.GetLootTable(LootTables[SelectedLootTableIndex].Name)) as ILootTableModel;

            // The monster api failed and returned null
            if (LootTables[SelectedLootTableIndex] == null)
            {
                LootTables[SelectedLootTableIndex] = CurrentLootTable;

                WeakReferenceMessenger.Default.Send(new MessageWindowConfiguration
                {
                    Message = "An error occurred while getting " + CurrentLootTable.Name + " data. Would you like to try again? " +
                    "Check you internet connection if you continue to see this message.",
                    IsOkVisible = false,
                    IsTrueFalseVisible = true,
                    Token = "ReloadLootTable"
                });
            }
            else
            {
                LootTables[SelectedLootTableIndex].IsDataComplete = true;
                CurrentLootTable = LootTables[SelectedLootTableIndex];
            }
        }

        //if (_isDebugOn)
        //{
        //    Console.Write(CurrentLootTable.Name);
        //}
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
            foreach(EquipmentSlotModel slot in _lootTables[_selectedLootTableIndex].EquipmentSlots)
            {
                slot.Name = slot.Equipment.Name;
            }
        }
    }

    private void AddLootSlot()
    {
        int selectedLootTableIndexBuffer = SelectedLootTableIndex;
        IEquipmentSlotModel equipmentSlot = _equipmentSlotFactoryService.GetEquipmentSlot();
        IEquipmentModel equipmentModel = _equipmentService.GetEquipment();
        equipmentSlot.Equipment = equipmentModel;
        IList<IEquipmentSlotModel> equipmentSlots = new List<IEquipmentSlotModel>();
        
        foreach(IEquipmentSlotModel equipmentSlotModel in CurrentLootTable.EquipmentSlots)
        {
            equipmentSlots.Add(equipmentSlotModel);
        }

        IEquipmentSlotModel newSlot = _equipmentSlotFactoryService.GetEquipmentSlot();
        equipmentSlots.Add(newSlot);

        IList<ILootTableModel> lootTables = new List<ILootTableModel>();
        foreach (ILootTableModel model in LootTables)
        {
            lootTables.Add(model);
        }

        lootTables[_selectedLootTableIndex].EquipmentSlots = equipmentSlots;

        _lootTablesRaw = LootTables = lootTables;
        SelectedLootTableIndex = selectedLootTableIndexBuffer;
        CurrentLootTable = LootTables[SelectedLootTableIndex];
    }

    private void AddLootTable()
    {
        IList<ILootTableModel> lootTables = new List<ILootTableModel>();
        foreach (ILootTableModel model in _lootTablesRaw)
        {
            lootTables.Add(model);
        }

        ILootTableModel newTable = new LootTableModel()
        {
            Name = "{{Name}}",
            Description = "{{Description}}",
            ImageSource = "Assets/Images/SwordIcon.png",
            EquipmentSlots = new List<IEquipmentSlotModel>()
        };
        lootTables.Add(newTable);

        IEnumerable<ILootTableModel> sortedLootTables = lootTables.OrderBy(x => x.Name);
        lootTables = sortedLootTables.ToList();
        _lootTablesRaw = LootTables = lootTables;
        SelectedLootTableIndex = lootTables.IndexOf(newTable);
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\
    public int SelectedLootTableIndex
    {
        get { return _selectedLootTableIndex; }
        set
        {
            if (_selectedLootTableIndex != value)
            {
                _selectedLootTableIndex = value;
                if (_selectedLootTableIndex > -1)
                {
                    GetLootTableDetails();
                }
                OnPropertyChanged();
            }
        }
    }

    public ILootTableModel CurrentLootTable
    {
        get 
        {
            if (_currentLootTable == null)
            {
                _currentLootTable = _lootTableFactoryService.GetLootTable();
            }

            return _currentLootTable; 
        }
        set
        {
            if (_currentLootTable != value)
            {
                _currentLootTable = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<ILootTableModel> LootTables
    {
        get { return _lootTables; }
        set
        {
            if (_lootTables != value)
            {
                _lootTables = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsEditEnabled
    {
        get { return _isEditEnabled; }
        set 
        {
            if(_isEditEnabled != value)
            {
                _isEditEnabled = value;
                if (_isEditEnabled)
                {
                    CurrentLootTable.LootTableSize = CurrentLootTable.EquipmentSlots.Count;
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
            if(_editIconSource != value)
            {
                _editIconSource = value;
                OnPropertyChanged();
            }
        }
    }

    public string Filter 
    { 
        get { return _filter; }
        set
        {
            if(_filter != value)
            {
                _filter = value;
                LootTables = _lootTableSearchAndFilter.Filter(_lootTablesRaw, _filter);
                OnPropertyChanged();
            }
        }
    }

    public ICommand ToggleEditCommand { get; set; }

    public ICommand AddLootSlotCommand { get; set; }

    public ICommand AddLootTableCommand { get; set; }
}
