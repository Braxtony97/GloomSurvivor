using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public Enums.MonsterTypeId MonsterType;
        
        [Range(1, 100)]
        public int Hp;
        
        [Range(1, 30)]
        public float Damage;
        
        [Range(0.5f, 1)]
        public float EffectiveDistance;
        
        public GameObject Prefab;
    }
}