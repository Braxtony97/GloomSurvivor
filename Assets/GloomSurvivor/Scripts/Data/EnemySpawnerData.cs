using System;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public Enums.MonsterTypeId Type; 
        public Vector3 Position;

        public EnemySpawnerData(string id, Enums.MonsterTypeId monsterTypeId, Vector3 position)
        {
            Id = id;
            Type = monsterTypeId;
            Position = position;
        }
    }
} 