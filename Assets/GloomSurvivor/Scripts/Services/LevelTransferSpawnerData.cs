using System;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [Serializable]
    public class LevelTransferSpawnerData
    {

        public string Id;
        public Enums.Scenes Scene;
        public Vector3 Position;

        public LevelTransferSpawnerData(string id, Enums.Scenes scene, Vector3 position)
        {
            Id = id;
            Scene = scene;
            Position = position;
        }
    }
}