using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DungeonAssistant.Business.Equipment.Interfaces;

namespace DungeonAssistant.Business.Equipment.Models;

public class LootTableModel : ObservableObject, ILootTableModel
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private int _id;
    private string _name;
    private string _description;
    private string _imageSource = "Assets//Images/SwordIcon.png";
    private int _lootTableSize;
    private IList<IEquipmentSlotModel> _equipmentSlots;

    public LootTableModel()
    {
        EquipmentSlots = new List<IEquipmentSlotModel>();

        
        WeakReferenceMessenger.Default.Register<EquipmentSlotModel>(this, (recipient, message) => { RemoveEquipmentSlot(message); });
    }

    private void RemoveEquipmentSlot(EquipmentSlotModel model)
    {
        EquipmentSlots.Remove(model);
        IList<IEquipmentSlotModel> buffer = new List<IEquipmentSlotModel>();
        foreach(IEquipmentSlotModel m in EquipmentSlots)
        {
            buffer.Add(m);
        }
        EquipmentSlots = buffer;
    }

    //**************************************************\\
    //******************* Properties *******************\\
    //**************************************************\\
    public int Id
    {
        get { return _id; }
        set
        {
            if (_id != value)
            {
                _id = value;
                OnPropertyChanged();
            }
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Description
    {
        get { return _description; }
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public string ImageSource
    {
        get { return _imageSource; }
        set
        {
            if (_imageSource != value)
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }
    }

    public int LootTableSize
    {
        get { return _lootTableSize; }
        set
        {
            if (_lootTableSize != value)
            {
                _lootTableSize = value;
                OnPropertyChanged();
            }
        }
    }

    public IList<IEquipmentSlotModel> EquipmentSlots
    {
        get { return _equipmentSlots; }
        set
        {
            if (_equipmentSlots != value)
            {
                _equipmentSlots = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsDataComplete { get; set; }
}
