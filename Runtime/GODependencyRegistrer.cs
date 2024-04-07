
namespace UnlockedStudios.DependencyResolver
{
    public static class GODependencyRegistrar
    {
        private static DependencyResolver _dependencyResolver;

        public static void Initialize()
        {
            if (_dependencyResolver == null)
                _dependencyResolver = new DependencyResolver();
        }

        public static void CreateSceneContainer(string sceneName)
        {
            _dependencyResolver.CreateSceneContainer(sceneName);
        }

        public static void RegisterSingleton<TService>()
        {
            _dependencyResolver.RegisterSingleton<TService>();
        }

        public static void RegisterSingleton<TService>(TService svc)
        {
            _dependencyResolver.RegisterSingleton(svc);
        }

        public static void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _dependencyResolver.RegisterSingleton<TService, TImplementation>();
        }

        public static void RegisterSingleton<TService, TImplementation>(TImplementation obj) where TImplementation : TService
        {
            _dependencyResolver.RegisterSingleton<TService, TImplementation>(obj);
        }

        public static void RegisterTransient<TService>()
        {
            _dependencyResolver.RegisterTransient<TService>();
        }

        public static void RegisterTransient<TService, TImplementation>(string sceneName = "") where TImplementation : TService
        {
            _dependencyResolver.RegisterTransient<TService, TImplementation>(sceneName);
        }

        public static void RegisterSingletonByScene<TService, TImplementation>(TImplementation obj, string sceneName)
            where TImplementation : TService
        {
            _dependencyResolver.RegisterSingletonByScene<TService, TImplementation>(obj, sceneName);
        }

        public static void UnloadServicesByScene(string sceneName)
        {
            _dependencyResolver.UnloadServicesByScene(sceneName);
        }

        public static bool HasScene(string sceneName)
        {
            return _dependencyResolver.HasScene(sceneName);
        }

        public static bool HasService<TService>()
        {
            return _dependencyResolver.HasService<TService>();
        }

        public static bool TryGetService<TService>(out TService service)
        {
            return _dependencyResolver.TryGetService<TService>(out service);
        }

        public static TService GetService<TService>()
        {
            return _dependencyResolver.GetService<TService>();
        }

        public static TService GetService<TService>(string sceneName)
        {
            return _dependencyResolver.GetService<TService>(sceneName);
        }
    }
}