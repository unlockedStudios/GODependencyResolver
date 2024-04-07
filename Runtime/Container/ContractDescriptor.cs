using System;

namespace UnlockedStudios.DependencyResolver.Contracts
{
    internal class ContractDescriptor
    {
        public Type ServiceType { get; }

        public Type ImplementationType { get; }

        public object Implementation { get; internal set; }

        public EnumServiceLifetime DepedencyType { get; }

        public ContractDescriptor(object implementation, EnumServiceLifetime dependencyType)
        {
            this.ServiceType = implementation.GetType();
            this.Implementation = implementation;
            this.DepedencyType = dependencyType;
        }

        public ContractDescriptor(Type serviceType, EnumServiceLifetime dependencyType)
        {
            this.ServiceType = serviceType;
            this.DepedencyType = dependencyType;
        }

        public ContractDescriptor(Type svcType, Type implementationType, EnumServiceLifetime dependencyType)
        {
            this.ServiceType = svcType;
            this.ImplementationType = implementationType;
            this.DepedencyType = dependencyType;
        }

        public ContractDescriptor(Type svcType, object implementation, EnumServiceLifetime dependencyType)
        {
            this.ServiceType = svcType;
            this.Implementation = implementation;
            this.DepedencyType = dependencyType;
        }

    }
}