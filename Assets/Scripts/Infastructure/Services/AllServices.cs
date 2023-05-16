namespace Infastructure.Services
{
    public class AllServices
    {
        public static AllServices Container => _instance ?? (new AllServices());
        
        private static AllServices _instance;


        public void RegisterSingle<TService>(TService Implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = Implementation;
        }


        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}