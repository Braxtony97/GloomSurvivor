using GloomSurvivor.Scripts.Characters.Enemy;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public Action Changed;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}