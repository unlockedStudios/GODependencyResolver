using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnlockedStudios.DependencyResolver.Contracts;

namespace UnlockedStudios.DependencyResolver
{
    internal class DiContainer
    {
        public string SceneName { get; protected set; }

        private List<ContractDescriptor> _serviceDescriptors;

        public DiContainer()
        {
            _serviceDescriptors = new List<ContractDescriptor>();
        }

        public DiContainer(string sceneName)
        {
            _serviceDescriptors = new List<ContractDescriptor>();
            SceneName = sceneName;
        }

        public object GetService(Type serviceType, string sceneName = "")
        {
            ContractDescriptor descriptor = _serviceDescriptors.SingleOrDefault(x => x.ServiceType == serviceType);

            // Verify there is a registered descriptor.
            if (descriptor == null)
                return null;

            // Verify an implementation exists, if so, return it.
            if (descriptor.Implementation != null)
                return descriptor.Implementation;

            // Verify its not an abstract or interface.
            Type actualType = descriptor.ImplementationType ?? descriptor.ServiceType;
            if (actualType.IsAbstract || actualType.IsInterface)
                throw new Exception("Cannot Instantiate abstract classes or interfaces");

            // Get constructor and populate its dependencies with other registered types.
            var constructorInfo = actualType.GetConstructors().First();
            var parameters = constructorInfo.GetParameters().Select(x => GetService(x.ParameterType)).ToArray();

            // Create instance of type.
            var implementation = Activator.CreateInstance(actualType);

            // Assign implementation.
            if (descriptor.DepedencyType == EnumServiceLifetime.Singleton)
                descriptor.Implementation = implementation;

            return implementation;
        }

        public void ContainerAdd(ContractDescriptor serviceDescriptor)
        {
            bool bOk = _serviceDescriptors.Any(x => x.ServiceType == serviceDescriptor.ServiceType);

            if (!bOk)
                _serviceDescriptors.Add(serviceDescriptor);
            else
                throw new Exception($"This container already has {serviceDescriptor.ServiceType} registered. Cannot add a second one.");
        }

        public void ContainerRemove(ContractDescriptor serviceDescriptor)
        {
            if (_serviceDescriptors.Contains(serviceDescriptor))
                _serviceDescriptors.Remove(serviceDescriptor);
        }

        public TService GetService<TService>()
        {
            object obj = GetService(typeof(TService));

            return (TService)obj;
        }

        public TService GetService<TService>(string sceneName)
        {
            object obj = GetService(typeof(TService), sceneName);

            return (TService)obj;
        }
    }
}