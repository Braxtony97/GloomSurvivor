using Assets.GloomSurvivor.Scripts.Logic.LevelTransferTriggerSpawner;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public Vector3 InitialPointHero;
        public List<LevelTransferSpawnerData> LevelTransferSpawners;
    }
}