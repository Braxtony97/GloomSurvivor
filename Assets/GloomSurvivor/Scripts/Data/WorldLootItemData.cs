using System;
using UnityEngine;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class WorldLootItemData
    {
        public string Id;
        public Vector3 Position;
        public int Value;
        public bool Picked;
    }
}