using GloomSurvivor.Scripts.Services;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData GetMonster(Enums.MonsterTypeId monsterId);
    }
}