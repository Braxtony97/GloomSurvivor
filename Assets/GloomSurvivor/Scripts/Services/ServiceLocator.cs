using GloomSurvivor.Scripts.Infrastructure.Factory;

namespace GloomSurvivor.Scripts.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());

        private static ServiceLocator _instance;

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService ResolveSingle<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}