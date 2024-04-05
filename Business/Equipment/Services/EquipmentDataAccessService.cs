using DungeonAssistant.Business.Commons.Models;
using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;
using DungeonAssistant.DataContracts.Interfaces;
using DungeonAssistant.DataContracts.Services;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace DungeonAssistant.Business.Equipment.Services;

public class EquipmentDataAccessService : IEquipmentDataAccessService
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private const string _baseUrl = "http://www.dnd5eapi.co";

    private ISQLiteDataAccess _sqLiteDataAccessService;
    private IEquipmentFactoryService _equipmentFactoryService;

    private const string NAME_COLUMN = "Name";
    private const string WEIGHT_COLUMN = "Weight";
    private const string COST_QUANTITY_COLUMN = "CostQuantity";
    private const string COST_UNITS_COLUMN = "CostUnits";
    private const string ONE_HAND_DAMAGE_DICE_COLUMN = "OneHandedDamageDice";
    private const string ONE_HAND_DAMAGE_TYPE_COLUMN = "OneHandedDamageType";
    private const string TWO_HAND_DAMAGE_DICE_COLUMN = "TwoHandedDamageDice";
    private const string TWO_HAND_DAMAGE_TYPE_COLUMN = "TwoHandedDamageType";
    private const string NORMAL_RANGE_COLUMN = "NormalRange";
    private const string LONG_RANGE_COLUMN = "LongRange";
    private const string EQUIPMENT_CATEGORY_COLUMN = "EquipmentCategory";
    private const string WEAPON_CATEGORY_COLUMN = "WeaponCategory";
    private const string WEAPON_RANGE_COLUMN = "WeaponRange";
    private const string ARMOR_CLASS_COLUMN = "ArmorClass";
    private const string IS_DEX_BONUS_COLUMN = "IsDexBonus";
    private const string MAX_DEX_BONUS_COLUMN = "MaxDexBonus";
    private const string STR_REQ_COLUMN = "StrengthRequirement";
    private const string IS_STEALTH_DISAD_COLUMN = "IsStealthDisadvantage";
    private const string TOOL_CATEGORY_COLUMN = "ToolCategory";
    private const string VEHICLE_CATEGORY_COLUMN = "VehicleCategory";
    private const string ARMOR_CATEGORY_COLUMN = "ArmorCategory";
    private const string GEAR_CATEGORY_COLUMN = "GearCategory";
    private const string PACKAGE_ID_COLUMN = "PackageId";
    private const string MAGIC_ITEM_ID_COLUMN = "MagicItemId";
    private const string DESCRIPTION_COLUMN = "Description";

    public EquipmentDataAccessService(IEquipmentFactoryService equipmentFactoryService)
    {
        _sqLiteDataAccessService = new SQLiteDataAccessService();
        _equipmentFactoryService = equipmentFactoryService;
    }

    //**************************************************\\
    //******************** Methods *********************\\
    //**************************************************\\

    public async Task<List<EquipmentModel>> GetAllEquipmentAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/equipment");
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EquipmentContainer>(rawJSON);
                    return data.Results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return null;
    }

    public async Task<EquipmentModel> GetEquipmentAsync(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                name = name.ToLower()
                    .Replace(" form", "")
                    .Replace(" ", "-")
                    .Replace("/", "-")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("'", "")
                    .Replace(":", "")
                    .Replace(",", "");
                HttpResponseMessage response = await client.GetAsync("/api/equipment/" + name);
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EquipmentModel>(rawJSON);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return default(EquipmentModel);
    }

    // New Methods
    public IEquipmentModel GetEquipment(int id)
    {
        throw new NotImplementedException();
    }

    public IEquipmentModel GetEquipment(string name)
    {
        throw new NotImplementedException();
    }

    public IList<IEquipmentModel> GetAllEquipment()
    {
        IList<IEquipmentModel> equipment = new List<IEquipmentModel>();
        StringBuilder equipmentQuery = new StringBuilder();
        equipmentQuery.Append("SELECT * FROM Equipment");

        // Get equipment data
        try
        {
            using (DataSet ds = _sqLiteDataAccessService.ExecuteQuery(equipmentQuery.ToString()))
            {
                if(ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        IEquipmentModel equipmentModel = _equipmentFactoryService.GetEquipment();
                        equipmentModel.Id = Convert.ToInt32(row["Id"]);
                        equipmentModel.Name = Convert.IsDBNull(row[NAME_COLUMN]) ? string.Empty : Convert.ToString(row[NAME_COLUMN]);
                        equipmentModel.Weight = Convert.IsDBNull(row[WEIGHT_COLUMN]) ? 0 : Convert.ToDouble(row[WEIGHT_COLUMN]);
                        equipmentModel.Cost.Quantity = Convert.IsDBNull(row[COST_QUANTITY_COLUMN]) ? 0 : Convert.ToInt64(row[COST_QUANTITY_COLUMN]);
                        equipmentModel.Cost.Unit = Convert.IsDBNull(row[COST_UNITS_COLUMN]) ? string.Empty : Convert.ToString(row[COST_UNITS_COLUMN]);

                        equipmentModel.Damage.DamageDice = Convert.IsDBNull(row[ONE_HAND_DAMAGE_DICE_COLUMN]) ? string.Empty : Convert.ToString(row[ONE_HAND_DAMAGE_DICE_COLUMN]);
                        
                        ICategoryModel oneHandedDamageCategoryModel = new CategoryModel();
                        oneHandedDamageCategoryModel.Name = Convert.IsDBNull(row[ONE_HAND_DAMAGE_TYPE_COLUMN]) ? string.Empty : Convert.ToString(row[ONE_HAND_DAMAGE_TYPE_COLUMN]);
                        equipmentModel.Damage.DamageType = oneHandedDamageCategoryModel;

                        equipmentModel.TwoHandedDamage.DamageDice = Convert.IsDBNull(row[TWO_HAND_DAMAGE_DICE_COLUMN]) ? string.Empty : Convert.ToString(row[TWO_HAND_DAMAGE_DICE_COLUMN]);
                        ICategoryModel twoHandedDamageCategoryModel = new CategoryModel();
                        twoHandedDamageCategoryModel.Name = Convert.IsDBNull(row[TWO_HAND_DAMAGE_TYPE_COLUMN]) ? string.Empty : Convert.ToString(row[TWO_HAND_DAMAGE_TYPE_COLUMN]);
                        equipmentModel.TwoHandedDamage.DamageType = twoHandedDamageCategoryModel;

                        equipmentModel.Range.Normal = Convert.IsDBNull(row[NORMAL_RANGE_COLUMN]) ? 0 : Convert.ToInt32(row[NORMAL_RANGE_COLUMN]);
                        equipmentModel.Range.Long = Convert.IsDBNull(row[LONG_RANGE_COLUMN]) ? 0 : Convert.ToInt32(row[LONG_RANGE_COLUMN]);

                        ICategoryModel equipmentCategoryModel = new CategoryModel();
                        equipmentCategoryModel.Name = Convert.IsDBNull(row[EQUIPMENT_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[EQUIPMENT_CATEGORY_COLUMN]);
                        equipmentModel.EquipmentCategory = equipmentCategoryModel;

                        equipmentModel.WeaponCategory = Convert.IsDBNull(row[WEAPON_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[WEAPON_CATEGORY_COLUMN]);
                        equipmentModel.WeaponRange = Convert.IsDBNull(row[WEAPON_RANGE_COLUMN]) ? string.Empty : Convert.ToString(row[WEAPON_RANGE_COLUMN]);

                        IArmorClassModel armorClassModel = new ArmorClassModel();
                        armorClassModel.Base = Convert.IsDBNull(row[ARMOR_CLASS_COLUMN]) ? 0 : Convert.ToInt32(row[ARMOR_CLASS_COLUMN]);
                        armorClassModel.DexBonus = Convert.IsDBNull(row[IS_DEX_BONUS_COLUMN]) ? false : Convert.ToBoolean(row[IS_DEX_BONUS_COLUMN]);
                        armorClassModel.MaxBonus = Convert.IsDBNull(row[MAX_DEX_BONUS_COLUMN]) ? 0 : Convert.ToInt32(row[MAX_DEX_BONUS_COLUMN]);
                        equipmentModel.ArmorClass = armorClassModel;

                        equipmentModel.StrengthRequirement = Convert.IsDBNull(row[STR_REQ_COLUMN]) ? 0 : Convert.ToInt32(row[STR_REQ_COLUMN]);
                        equipmentModel.IsStealthDisadvantage = Convert.IsDBNull(row[IS_STEALTH_DISAD_COLUMN]) ? false : Convert.ToBoolean(row[IS_STEALTH_DISAD_COLUMN]);
                        equipmentModel.ToolCategory = Convert.IsDBNull(row[TOOL_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[TOOL_CATEGORY_COLUMN]);
                        equipmentModel.VehicleCategory = Convert.IsDBNull(row[VEHICLE_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[VEHICLE_CATEGORY_COLUMN]);
                        equipmentModel.ArmorCategory = Convert.IsDBNull(row[ARMOR_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[ARMOR_CATEGORY_COLUMN]);

                        ICategoryModel gearCategoryModel = new CategoryModel();
                        gearCategoryModel.Name = Convert.IsDBNull(row[GEAR_CATEGORY_COLUMN]) ? string.Empty : Convert.ToString(row[GEAR_CATEGORY_COLUMN]);
                        equipmentModel.GearCategory = gearCategoryModel;

                        equipmentModel.PackageId = Convert.IsDBNull(row["PackageId"]) ? 0 : Convert.ToInt32(row["PackageId"]);

                        equipmentModel.Description.Clear();
                        equipmentModel.IsDataComplete = true;
                        equipment.Add(equipmentModel);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Get description data
        StringBuilder descriptionQuery = new StringBuilder();
        descriptionQuery.Append("SELECT * FROM EquipmentDescriptions");

        try
        {
            using (DataSet ds = _sqLiteDataAccessService.ExecuteQuery(descriptionQuery.ToString()))
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int id = Convert.ToInt32(row["EquipmentId"]);
                        for (int i = 0; i < equipment.Count; i++)
                        {
                            if(equipment[i].Id == id)
                            {
                                DescriptionModel description = new DescriptionModel(Convert.ToInt32(row["Id"]), Convert.ToString(row[DESCRIPTION_COLUMN]));
                                equipment[i].Description.Add(description);
                                break;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return equipment;
    }

    public bool SaveEquipment(IEquipmentModel equipment)
    {
        bool success = false;

        if(equipment.Id < 1)
        {
            success = InsertEquipment(equipment);

            if(success == false)
            {
                return false;
            }

            foreach(DescriptionModel description in equipment.Description)
            {
                success = InsertEquipmentDescriptions(equipment, description);

                if (success == false)
                {
                    return false;
                }
            }
        }
        else
        {
            success = UpdateEquipment(equipment);

            if (success == false)
            {
                return false;
            }

            foreach (DescriptionModel description in equipment.Description)
            {
                if(description.Id > 0)
                {
                    success = UpdateEquipmentDescriptions(description);
                }
                else
                {
                    success = InsertEquipmentDescriptions(equipment, description);
                }

                if (success == false)
                {
                    return false;
                }
            }
        }

        return success;
    }

    public bool SaveEquipment(IList<IEquipmentModel> equipment)
    {
        bool success = true;

        foreach (IEquipmentModel equipmentModel in equipment)
        {
            success = SaveEquipment(equipment);
            if(success == false)
            {
                Console.WriteLine("An error occurred during SaveEquipment.");
                break;
            }
        }

        return success;
    }

    public bool DeleteEquipment(IEquipmentModel equipment)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEquipment(IList<IEquipmentModel> equipment)
    {
        throw new NotImplementedException();
    }

    private bool InsertEquipment(IEquipmentModel equipment)
    {
        bool success = false;

        StringBuilder header = new StringBuilder();
        StringBuilder values = new StringBuilder();
        header.Append("INSERT INTO Equipment (");
        values.Append(" VALUES (");

        if (equipment.Name != null)
        {
            header.Append(NAME_COLUMN + ", ");
            values.Append("\"" + equipment.Name + "\", ");
        }

        if (equipment.Weight != 0)
        {
            header.Append(WEIGHT_COLUMN + ", ");
            values.Append(equipment.Weight + ", ");
        }

        if (equipment.Cost.Quantity != 0)
        {
            header.Append(COST_QUANTITY_COLUMN + ", ");
            values.Append(equipment.Cost.Quantity + ", ");
        }

        if (equipment.Cost.Unit != null)
        {
            header.Append(COST_UNITS_COLUMN + ", ");
            values.Append("\"" + equipment.Cost.Unit + "\", ");
        }

        if (equipment.Damage.DamageDice != null)
        {
            header.Append(ONE_HAND_DAMAGE_DICE_COLUMN + ", ");
            values.Append("\"" + equipment.Damage.DamageDice + "\", ");
        }

        if (equipment.Damage.DamageType.Name != null)
        {
            header.Append(ONE_HAND_DAMAGE_TYPE_COLUMN + ", ");
            values.Append("\"" + equipment.Damage.DamageType.Name + "\", ");
        }

        if (equipment.TwoHandedDamage.DamageDice != null)
        {
            header.Append(TWO_HAND_DAMAGE_DICE_COLUMN + ", ");
            values.Append("\"" + equipment.TwoHandedDamage.DamageDice + "\", ");
        }

        if (equipment.TwoHandedDamage.DamageType.Name != null)
        {
            header.Append(TWO_HAND_DAMAGE_TYPE_COLUMN + ", ");
            values.Append("\"" + equipment.TwoHandedDamage.DamageType.Name + "\", ");
        }

        if (equipment.Range.Normal != null)
        {
            header.Append(NORMAL_RANGE_COLUMN + ", ");
            values.Append(equipment.Range.Normal + ", ");
        }

        if (equipment.Range.Long != null)
        {
            header.Append(LONG_RANGE_COLUMN + ", ");
            values.Append(equipment.Range.Long + ", ");
        }

        if (equipment.ArmorClass.Base != null)
        {
            header.Append(ARMOR_CLASS_COLUMN + ", ");
            values.Append(equipment.ArmorClass.Base + ", ");
        }

        if (equipment.ArmorClass.DexBonus != false)
        {
            header.Append(IS_DEX_BONUS_COLUMN + ", ");
            values.Append(equipment.ArmorClass.DexBonus + ", ");
        }

        if (equipment.ArmorClass.MaxBonus != null)
        {
            header.Append(MAX_DEX_BONUS_COLUMN + ", ");
            values.Append(equipment.ArmorClass.MaxBonus + ", ");
        }

        if (equipment.StrengthRequirement != 0)
        {
            header.Append(STR_REQ_COLUMN + ", ");
            values.Append(equipment.StrengthRequirement + ", ");
        }

        if (equipment.IsStealthDisadvantage != false)
        {
            header.Append(IS_STEALTH_DISAD_COLUMN + ", ");
            values.Append(equipment.IsStealthDisadvantage + ", ");
        }

        if (equipment.EquipmentCategory.Name != null)
        {
            header.Append(EQUIPMENT_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.EquipmentCategory.Name + "\", ");
        }

        if (equipment.WeaponRange != null)
        {
            header.Append(WEAPON_RANGE_COLUMN + ", ");
            values.Append("\"" + equipment.WeaponRange + "\", ");
        }

        if (equipment.WeaponCategory != null)
        {
            header.Append(WEAPON_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.WeaponCategory + "\", ");
        }

        if (equipment.ToolCategory != null)
        {
            header.Append(TOOL_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.ToolCategory + "\", ");
        }

        if (equipment.VehicleCategory != null)
        {
            header.Append(VEHICLE_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.VehicleCategory + "\", ");
        }

        if (equipment.ArmorCategory != null)
        {
            header.Append(ARMOR_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.ArmorCategory + "\", ");
        }

        if (equipment.GearCategory.Name != null)
        {
            header.Append(GEAR_CATEGORY_COLUMN + ", ");
            values.Append("\"" + equipment.GearCategory.Name + "\", ");
        }

        header.Remove(header.Length - 2, 2);
        header.Append(") ");

        values.Remove(values.Length - 2, 2);
        values.Append(") ");

        try
        {
            success = _sqLiteDataAccessService.ExecuteNonQuery(header.ToString() + values.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return success;
    }

    private bool UpdateEquipment(IEquipmentModel equipment)
    {
        bool success = false;

        StringBuilder header = new StringBuilder();
        StringBuilder where = new StringBuilder();

        header.Append("UPDATE Equipment SET ");
        header.Append(NAME_COLUMN + " = " + "\"" + equipment.Name + "\", ");

        if (equipment.Weight != 0)
        {
            header.Append(WEIGHT_COLUMN + " = " + equipment.Weight + ", ");
        }

        if (equipment.Cost.Quantity != 0)
        {
            header.Append(COST_QUANTITY_COLUMN + " = " + equipment.Cost.Quantity + ", ");
        }

        if (equipment.Cost.Unit != null)
        {
            header.Append(COST_UNITS_COLUMN + " = " + "\"" + equipment.Cost.Unit + "\", ");
        }

        if (equipment.Damage.DamageDice != null && equipment.Damage.DamageDice != string.Empty)
        {
            header.Append(ONE_HAND_DAMAGE_DICE_COLUMN + " = " + "\"" + equipment.Damage.DamageDice + "\", ");
        }

        if (equipment.Damage.DamageType.Name != null && equipment.Damage.DamageType.Name != string.Empty)
        {
            header.Append(ONE_HAND_DAMAGE_TYPE_COLUMN + " = " + "\"" + equipment.Damage.DamageType.Name + "\", ");
        }

        if (equipment.TwoHandedDamage.DamageDice != null && equipment.TwoHandedDamage.DamageDice != string.Empty)
        {
            header.Append(TWO_HAND_DAMAGE_DICE_COLUMN + " = " + "\"" + equipment.TwoHandedDamage.DamageDice + "\", ");
        }

        if (equipment.TwoHandedDamage.DamageType.Name != null && equipment.TwoHandedDamage.DamageType.Name != string.Empty)
        {
            header.Append(TWO_HAND_DAMAGE_TYPE_COLUMN + " = " + "\"" + equipment.TwoHandedDamage.DamageType.Name + "\", ");
        }

        if (equipment.Range.Normal != null)
        {
            header.Append(NORMAL_RANGE_COLUMN + " = " + equipment.Range.Normal + ", ");
        }

        if (equipment.Range.Long != null)
        {
            header.Append(LONG_RANGE_COLUMN + " = " + equipment.Range.Long + ", ");
        }

        if (equipment.ArmorClass.Base != null)
        {
            header.Append(ARMOR_CLASS_COLUMN + " = " + equipment.ArmorClass.Base + ", ");
        }

        if (equipment.ArmorClass.DexBonus != false)
        {
            header.Append(IS_DEX_BONUS_COLUMN + " = " + equipment.ArmorClass.DexBonus + ", ");
        }

        if (equipment.ArmorClass.MaxBonus != null)
        {
            header.Append(MAX_DEX_BONUS_COLUMN + " = " + equipment.ArmorClass.MaxBonus + ", ");
        }

        if (equipment.StrengthRequirement != 0)
        {
            header.Append(STR_REQ_COLUMN + " = " + equipment.StrengthRequirement + ", ");
        }

        if (equipment.IsStealthDisadvantage != false)
        {
            header.Append(IS_STEALTH_DISAD_COLUMN + " = " + equipment.IsStealthDisadvantage + ", ");
        }

        if (equipment.EquipmentCategory.Name != null && equipment.EquipmentCategory.Name != string.Empty)
        {
            header.Append(EQUIPMENT_CATEGORY_COLUMN + " = " + "\"" + equipment.EquipmentCategory.Name + "\", ");
        }

        if (equipment.WeaponRange != null && equipment.WeaponRange != string.Empty)
        {
            header.Append(WEAPON_RANGE_COLUMN + " = " + "\"" + equipment.WeaponRange + "\", ");
        }

        if (equipment.WeaponCategory != null && equipment.WeaponCategory != string.Empty)
        {
            header.Append(WEAPON_CATEGORY_COLUMN + " = " + "\"" + equipment.WeaponCategory + "\", ");
        }

        if (equipment.ToolCategory != null && equipment.ToolCategory != string.Empty)
        {
            header.Append(TOOL_CATEGORY_COLUMN + " = " + "\"" + equipment.ToolCategory + "\", ");
        }

        if (equipment.VehicleCategory != null && equipment.VehicleCategory != string.Empty)
        {
            header.Append(VEHICLE_CATEGORY_COLUMN + " = " + "\"" + equipment.VehicleCategory + "\", ");
        }

        if (equipment.ArmorCategory != null && equipment.ArmorCategory != string.Empty)
        {
            header.Append(ARMOR_CATEGORY_COLUMN + " = " + "\"" + equipment.ArmorCategory + "\", ");
        }

        if (equipment.GearCategory.Name != null && equipment.GearCategory.Name != string.Empty)
        {
            header.Append(GEAR_CATEGORY_COLUMN + " = " + "\"" + equipment.GearCategory.Name + "\", ");
        }

        header.Remove(header.Length - 2, 2);
        where.Append(" WHERE Id = " + equipment.Id);

        // Debugging purposes
        //Console.WriteLine(header.ToString() + where.ToString());

        try
        {
            success = _sqLiteDataAccessService.ExecuteNonQuery(header.ToString() + where.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return success;
    }

    private bool UpdateEquipment(IList<IEquipmentModel> equipment)
    {
        bool success = false;

        foreach (IEquipmentModel equipmentModel in equipment)
        {
            success = UpdateEquipment(equipment);
            if (success == false)
            {
                Console.WriteLine("An error occurred during SaveEquipment.");
                break;
            }
        }

        return success;
    }

    private bool InsertEquipmentDescriptions(IEquipmentModel equipment, DescriptionModel description)
    {
        bool success = false;

        StringBuilder header = new StringBuilder();
        StringBuilder values = new StringBuilder();
        header.Append("INSERT INTO EquipmentDescriptions (EquipmentId, Description)");
        values.Append(" SELECT Id, ");

        values.Append("\"" + description.Text + "\"");

        values.Append(" FROM Equipment WHERE Name = \"" + equipment.Name + "\"");

        // Debugging purposes
        Console.WriteLine(header.ToString() + values.ToString());

        try
        {
            success = _sqLiteDataAccessService.ExecuteNonQuery(header.ToString() + values.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return success;
    }

    private bool UpdateEquipmentDescriptions(DescriptionModel description)
    {
        bool success = false;

        StringBuilder header = new StringBuilder();
        StringBuilder where = new StringBuilder();
        header.Append("UPDATE EquipmentDescriptions SET Description = \"" + description.Text + "\"");
        where.Append(" WHERE Id = " + description.Id);

        // Debugging purposes
        Console.WriteLine(header.ToString() + where.ToString());

        try
        {
            success = _sqLiteDataAccessService.ExecuteNonQuery(header.ToString() + where.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return success;
    }


    //**************************************************\\
    //****************** Nested Class ******************\\
    //**************************************************\\
    private class EquipmentContainer
    {
        public EquipmentContainer(int count, List<EquipmentModel> results)
        {
            Count = count;
            Results = results;
        }

        public int Count { get; set; }
        public List<EquipmentModel> Results { get; set; }
    }
}
