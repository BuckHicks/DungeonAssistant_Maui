
using DungeonAssistant.Business.MonsterManual.Models;

namespace DungeonAssistant.Business.MonsterManual.Interfaces;

public interface IMonsterModel
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Size { get; set; }
    string MonsterType { get; set; }
    string MonsterSubtype { get; set; }
    string Alignment { get; set; }
    List<ArmorClassModel> ArmorClass { get; set; }
    int HitPoints { get; set; }
    string HitPointsCalculation { get; set; }
    SpeedModel Speed { get; set; }
    int Strength { get; set; }
    int StrengthModifier { get; set; }
    int Dexterity { get; set; }
    int DexterityModifier { get; set; }
    int Constitution { get; set; }
    int ConstitutionModifier { get; set; }
    int Intelligence { get; set; }
    int IntelligenceModifier { get; set; }
    int Wisdom { get; set; }
    int WisdomModifier { get; set; }
    int Charisma { get; set; }
    int CharismaModifier { get; set; }
    bool IsDataComplete { get; set; }
    List<ProficiencyModel> Proficiencies { get; set; }
    double ChallengeRating { get; set; }
    int Xp { get; set; }
}
