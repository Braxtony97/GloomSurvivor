using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}