using System;
using System.Collections.Generic;

namespace GloomSurvivor.Scripts.Data
{
    [Serializable]
    public class WorldLootData
    {
        public List<WorldLootItemData> ActiveLoots = new();
    }
}