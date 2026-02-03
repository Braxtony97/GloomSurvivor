using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GloomSurvivor.Scripts.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _complitedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles;

        public async Task<T> Load<T>(AssetReference assetRefernce)
        {
            if (_complitedCache.TryGetValue(assetRefernce.AssetGUID, out AsyncOperationHandle complitedHandle))
                return (T)complitedHandle.Result;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetRefernce);
            handle.Completed += h =>
            {
                _complitedCache[assetRefernce.AssetGUID] = h;
            };

            AddHandle(assetRefernce.AssetGUID, handle);

            return await handle.Task;
        }


        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle)
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

    }
}