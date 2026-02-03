using GloomSurvivor.Scripts.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GloomSurvivor.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        Task<T> Load<T>(AssetReference assetRefernce);
    }
}