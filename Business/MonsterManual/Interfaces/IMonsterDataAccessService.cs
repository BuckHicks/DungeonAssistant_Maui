
using DungeonAssistant.Business.MonsterManual.Models;

namespace DungeonAssistant.Business.MonsterManual.Interfaces;

public interface IMonsterDataAccessService
{
    Task<List<MonsterModel>> GetAllMonsters();
    Task<MonsterModel> GetMonster(string name);
}
