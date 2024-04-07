using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnlockedStudios.DependencyResolver.Contracts;

namespace UnlockedStudios.DependencyResolver
{
    public class DependencyResolver
    {
        private List<ContractDescriptor> _serviceDescriptors;

        private List<DiContainer> _diContainers;

        private string defaultContainer = "defaultContainer";

        public DependencyResolver()
        {
            _serviceDescriptors = new List<ContractDescriptor>();
            _diContainers = new List<DiContainer>();
            CreateSceneContainer(defaultContainer);
        }

        public void CreateSceneContainer(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
                throw new UnityException("Scene name cannot be null or empty!");

            DiContainer newScene = new DiContainer(sceneName);
            _diContainers.Add(newScene);
        }

        public void RegisterSingleton<TService>()
        {
            DiContainer diContainer = _diContainers.First(x => x.SceneName == defaultContainer);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), EnumServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TService>(TService svc)
        {
            DiContainer diContainer = _diContainers.First(x => x.SceneName == defaultContainer);
            diContainer.ContainerAdd(new ContractDescriptor(svc, EnumServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            DiContainer diContainer = _diContainers.First(x => x.SceneName == defaultContainer);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), typeof(TImplementation), EnumServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TService, TImplementation>(TImplementation obj) where TImplementation : TService
        {
            DiContainer diContainer = _diContainers.First(x => x.SceneName == defaultContainer);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), obj, EnumServiceLifetime.Singleton));
        }

        public void RegisterTransient<TService>()
        {
            DiContainer diContainer = _diContainers.First(x => x.SceneName == defaultContainer);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), EnumServiceLifetime.Transient));
        }

        public void RegisterTransient<TService, TImplementation>(string sceneName = "") where TImplementation : TService
        {
            string sceneRef = string.IsNullOrEmpty(sceneName) ? defaultContainer : sceneName;

            DiContainer diContainer = _diContainers.First(x => x.SceneName == sceneRef);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), typeof(TImplementation), EnumServiceLifetime.Transient));
        }

        public void RegisterSingletonByScene<TService, TImplementation>(TImplementation obj, string sceneName)
            where TImplementation : TService
        {
            if (string.IsNullOrEmpty(sceneName))
                throw new Exception("sceneName cannot be Null or Empty. A valid name is required.");


            DiContainer diContainer = _diContainers.First(x => x.SceneName == sceneName);
            diContainer.ContainerAdd(new ContractDescriptor(typeof(TService), obj, EnumServiceLifetime.Singleton));
        }

        public void UnloadServicesByScene(string sceneName)
        {
            _diContainers.RemoveAll(x => x.SceneName == sceneName);
        }

        public bool HasScene(string sceneName)
        {
            return _diContainers.Any(x => x.SceneName == sceneName);
        }

        public bool HasService<TService>()
        {
            foreach (var di in _diContainers)
            {
                var obj = di.GetService(typeof(TService));
                if (obj != null && obj.Equals(null) == false)
                    return true;
            }

            return false;
        }

        public bool TryGetService<TService>(out TService service)
        {
            foreach (DiContainer di in _diContainers)
            {
                object obj = di.GetService(typeof(TService));
                if (obj != null && obj.Equals(null) == false)
                {
                    service = (TService)obj;
                    return true;
                }
            }
            service = default(TService);
            return false;
        }

        public TService GetService<TService>()
        {
            foreach(var di in _diContainers)
            {
                var obj = di.GetService(typeof(TService));
                if (obj != null && obj.Equals(null) == false)
                    return (TService)obj;
            }

            throw new UnityException($"Service { typeof(TService).ToString() } is not registered.");
        }

        public TService GetService<TService>(string sceneName)
        {
            DiContainer di = _diContainers.First(x => x.SceneName == sceneName);
            var obj = di.GetService<TService>(sceneName);
            if (obj != null && obj.Equals(null))
                return (TService)obj;

            throw new UnityException($"Service { typeof(TService).ToString() } is not registered.");
        }
    }
}