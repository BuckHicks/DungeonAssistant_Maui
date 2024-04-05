using System.Data;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;
using DungeonAssistant.DataContracts.Interfaces;
using DungeonAssistant.DataContracts.Services;

namespace DungeonAssistant.Business.Equipment.Services;

public class LootTableDataAccessService : ILootTableDataAccessService
{
    private ISQLiteDataAccess _sqLiteDataAccessService;
    private ILootTableFactoryService _lootTableFactoryService;
    private IEquipmentSlotFactoryService _equipmentSlotFactoryService;
    private IEquipmentFactoryService _equipmentFactoryService;

    private const string _lootTableName = "LootTables";
    private const string _descriptionTableName = "LootTablesDescriptions";

    private const string _baseUrl = "http://www.dnd5eapi.co";

    public LootTableDataAccessService(ILootTableFactoryService lootTableFactoryService, IEquipmentSlotFactoryService equipmentSlotFactoryService,
        IEquipmentFactoryService equipmentFactoryService)
    {
        _sqLiteDataAccessService = new SQLiteDataAccessService();
        _lootTableFactoryService = lootTableFactoryService;
        _equipmentSlotFactoryService = equipmentSlotFactoryService;
        _equipmentFactoryService = equipmentFactoryService;
    }

    public ILootTableModel GetLootTable(int id)
    {
        throw new NotImplementedException();
    }

    public ILootTableModel GetLootTable(string name)
    {
        ILootTableModel lootTable = _lootTableFactoryService.GetLootTable();
        lootTable.Name = name;
        
        string query = "SELECT " +
            "ES.Id AS EquipmentSlotId, " +
            "ES.Multiplier, " +
            "ES.LootTableId, " +
            "ES.[Index], " +
            "E.Id AS EquipmentId, " +
            "E.name AS Name, " +
            "E.Weight AS Weight, " +
            "E.CostQuantity, " +
            "E.CostUnits, " +
            "E.OneHandedDamageDice, " +
            "E.OneHandedDamageType, " +
            "E.TwoHandedDamageDice, " +
            "E.TwoHandedDamageType, " +
            "E.NormalRange, " +
            "E.LongRange, " +
            "E.EquipmentCategory, " +
            "E.WeaponRange, " +
            "E.WeaponCategory, " +
            "E.ToolCategory, " +
            "E.VehicleCategory, " +
            "E.ArmorCategory, " +
            "E.GearCategory " +
        "FROM LootTables AS LT " +
            "INNER JOIN EquipmentSlots AS ES " +
                "ON LT.Id = ES.LootTableId " +
            "INNER JOIN Equipment AS E " +
                "ON E.Id = ES.EquipmentId " +
        "WHERE LT.Name = '" + name + "' " +
        "ORDER BY ES.[Index]";

        try
        {
            using (DataSet ds = _sqLiteDataAccessService.ExecuteQuery(query))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        IEquipmentSlotModel equipmentSlotModel = _equipmentSlotFactoryService.GetEquipmentSlot();
                        IEquipmentModel equipmentModel = _equipmentFactoryService.GetEquipment();

                        equipmentSlotModel.Id = Convert.IsDBNull(row["EquipmentSlotId"]) ? 0 : Convert.ToInt32(row["EquipmentSlotId"]);
                        equipmentSlotModel.Multiplier = Convert.IsDBNull(row["Multiplier"]) ? 0 : Convert.ToInt32(row["Multiplier"]);
                        equipmentSlotModel.LootTableId = Convert.IsDBNull(row["LootTableId"]) ? 0 : Convert.ToInt32(row["LootTableId"]);
                        equipmentSlotModel.Index = Convert.IsDBNull(row["Index"]) ? 0 : Convert.ToInt32(row["Index"]);

                        equipmentModel.Id = Convert.IsDBNull(row["EquipmentId"]) ? 0 : Convert.ToInt32(row["EquipmentId"]);
                        equipmentModel.Name = Convert.IsDBNull(row["Name"]) ? string.Empty : Convert.ToString(row["Name"]);
                        equipmentModel.Weight = Convert.IsDBNull(row["Weight"]) ? 0 : Convert.ToDouble(row["Weight"]);
                        equipmentModel.Cost.Quantity = Convert.IsDBNull(row["CostQuantity"]) ? 0 : Convert.ToInt32(row["CostQuantity"]);
                        equipmentModel.Cost.Unit = Convert.IsDBNull(row["CostUnits"]) ? string.Empty : Convert.ToString(row["CostUnits"]);
                        equipmentModel.Damage.DamageDice = Convert.IsDBNull(row["OneHandedDamageDice"]) ? string.Empty : Convert.ToString(row["OneHandedDamageDice"]);
                        equipmentModel.Damage.DamageType.Name = Convert.IsDBNull(row["OneHandedDamageType"]) ? string.Empty : Convert.ToString(row["OneHandedDamageType"]);
                        equipmentModel.TwoHandedDamage.DamageDice = Convert.IsDBNull(row["TwoHandedDamageDice"]) ? string.Empty : Convert.ToString(row["TwoHandedDamageDice"]);
                        equipmentModel.TwoHandedDamage.DamageType.Name = Convert.IsDBNull(row["TwoHandedDamageType"]) ? string.Empty : Convert.ToString(row["TwoHandedDamageType"]);
                        equipmentModel.Range.Normal = Convert.IsDBNull(row["NormalRange"]) ? (int?)null : Convert.ToInt32(row["NormalRange"]);
                        equipmentModel.Range.Long = Convert.IsDBNull(row["LongRange"]) ? (int?)null : Convert.ToInt32(row["LongRange"]);
                        equipmentModel.EquipmentCategory.Name = Convert.IsDBNull(row["EquipmentCategory"]) ? string.Empty : Convert.ToString(row["EquipmentCategory"]);
                        equipmentModel.WeaponRange = Convert.IsDBNull(row["WeaponRange"]) ? string.Empty : Convert.ToString(row["WeaponRange"]);
                        equipmentModel.WeaponCategory = Convert.IsDBNull(row["WeaponCategory"]) ? string.Empty : Convert.ToString(row["WeaponCategory"]);
                        equipmentModel.ToolCategory = Convert.IsDBNull(row["ToolCategory"]) ? string.Empty : Convert.ToString(row["ToolCategory"]);
                        equipmentModel.VehicleCategory = Convert.IsDBNull(row["VehicleCategory"]) ? string.Empty : Convert.ToString(row["VehicleCategory"]);
                        equipmentModel.ArmorCategory = Convert.IsDBNull(row["ArmorCategory"]) ? string.Empty : Convert.ToString(row["ArmorCategory"]);
                        equipmentModel.GearCategory.Name = Convert.IsDBNull(row["GearCategory"]) ? string.Empty : Convert.ToString(row["GearCategory"]);

                        equipmentSlotModel.Equipment = equipmentModel;

                        lootTable.EquipmentSlots.Add(equipmentSlotModel);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return lootTable;
    }

    public IList<ILootTableModel> GetLootTables()
    {
        IList<ILootTableModel> lootTables = new List<ILootTableModel>();

        string query =  "SELECT " +
                            "LT.Id " +
                            ", LT.PackageId " +
	                            ", LT.Name " +
	                            ", D.Id AS DescriptionId " +
	                            ", D.Description " +
                        " FROM " + _lootTableName + " AS LT " +
                            " INNER JOIN " + _descriptionTableName + " AS D " +
                                " ON LT.Id = D.LootTableId ";
        try
        {
            using (DataSet ds = _sqLiteDataAccessService.ExecuteQuery(query))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ILootTableModel lootTable = _lootTableFactoryService.GetLootTable();
                    lootTable.Id = Convert.IsDBNull(row["Id"]) ? 0 : Convert.ToInt32(row["Id"]);
                    lootTable.Name = Convert.IsDBNull(row["Name"]) ? string.Empty : Convert.ToString(row["Name"]);
                    lootTable.Description = Convert.IsDBNull(row["Description"]) ? string.Empty : Convert.ToString(row["Description"]);
                    lootTables.Add(lootTable);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return lootTables;
    }

    public bool SaveLootTable(ILootTableModel lootTable)
    {
        throw new NotImplementedException();
    }

    public bool SaveLootTables(IList<ILootTableModel> lootTable)
    {
        throw new NotImplementedException();
    }

    public bool UpdateLootTable(ILootTableModel lootTable)
    {
        throw new NotImplementedException();
    }

    public bool UpdateLootTables(IList<ILootTableModel> lootTable)
    {
        throw new NotImplementedException();
    }

    public bool DeleteLootTable(ILootTableModel lootTable)
    {
        throw new NotImplementedException();
    }

    public bool DeleteLootTables(IList<ILootTableModel> lootTable)
    {
        throw new NotImplementedException();
    }


    //**************************************************\\
    //****************** Nested Class ******************\\
    //**************************************************\\
    private class LootTableContainer
    {
        public LootTableContainer(int count, List<LootTableModel> results)
        {
            Count = count;
            Results = results;
        }

        public int Count { get; set; }
        public List<LootTableModel> Results { get; set; }
    }
}
