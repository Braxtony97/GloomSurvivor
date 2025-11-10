using GloomSurvivor.Scripts.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        Task Load<T>(string address);
    }
}