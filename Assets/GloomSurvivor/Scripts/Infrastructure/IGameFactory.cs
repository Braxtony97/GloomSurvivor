using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}