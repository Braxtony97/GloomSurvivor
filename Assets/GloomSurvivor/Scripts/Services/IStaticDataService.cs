using GloomSurvivor.Scripts.Services;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData GetMonster(Enums.MonsterTypeId monsterId);
        LevelStaticData GetLevel(string sceneKey);
    }
}