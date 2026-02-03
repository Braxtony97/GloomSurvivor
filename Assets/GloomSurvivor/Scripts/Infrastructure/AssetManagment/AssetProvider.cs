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
        
        /// <summary>
        /// Для каждого ресурса assetRefernce.AssetGUID содержит все его асинхронные операции, которые вызвались
        /// </summary>
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles;
        
        public async Task<T> Load<T>(AssetReference assetRefernce)
        {
            // Если в кэше есть такой указатель, то вернет
            if (_complitedCache.TryGetValue(assetRefernce.AssetGUID, out AsyncOperationHandle complitedHandle))
                return (T)complitedHandle.Result;
            
            // handle: ссылка на объект, который обязятально нужно релизить (совобождать)
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

        /// <summary>
        /// Метод для освобождения ресурсов
        /// </summary>
        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle resourceHandle in resourceHandles) 
                Addressables.Release(resourceHandle);
        }

        /// <summary>
        /// Добавление листа запросов в словарь по ключу
        /// </summary>
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