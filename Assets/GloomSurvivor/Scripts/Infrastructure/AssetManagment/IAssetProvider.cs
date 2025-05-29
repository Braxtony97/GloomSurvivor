using GloomSurvivor.Scripts.Services;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}