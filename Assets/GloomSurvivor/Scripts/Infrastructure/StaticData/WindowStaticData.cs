using System.Collections.Generic;
using UnityEngine;

namespace Assets.GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticDataList", menuName = "StaticData/WindowStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}
