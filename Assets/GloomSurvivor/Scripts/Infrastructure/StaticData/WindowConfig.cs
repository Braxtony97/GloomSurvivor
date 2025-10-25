using Assets.GloomSurvivor.Scripts.UI.Windows;
using GloomSurvivor.Scripts.Infrastructure;
using System;

namespace Assets.GloomSurvivor.Scripts.Infrastructure.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public Enums.WindowId WindowId;
        public WindowBase Prefab;
    }
}