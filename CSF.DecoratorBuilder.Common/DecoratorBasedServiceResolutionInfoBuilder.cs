using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A non-generic builder which creates an instance of <see cref="DecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances of this builder class must be used a maximum of once.  They are not reusable.
    /// </para>
    /// </remarks>
    public class DecoratorBasedServiceResolutionInfoBuilder : IDecoratorBuilder
    {
        readonly Type serviceType;
        readonly IEnumerable<ITypedResolvable> globalParameters;

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo ResolutionInfo { get; }

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImpl<TInitialImpl>(params ITypedResolvable[] parameters) where TInitialImpl : class
            => UsingInitialImpl<TInitialImpl>(null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImpl<TInitialImpl>(Func<IServiceProvider, IEnumerable<ITypedResolvable>, TInitialImpl> factoryFunction, params ITypedResolvable[] parameters) where TInitialImpl : class
        {
            AssertObjectTypeImplementsServiceType(typeof(TInitialImpl));

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction(services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(typeof(TInitialImpl), parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImplType(Type initialImplType, params ITypedResolvable[] parameters)
            => UsingInitialImplType(initialImplType, null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImplType(Type initialImplType, Func<IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunction, params ITypedResolvable[] parameters)
        {
            AssertObjectTypeImplementsServiceType(initialImplType);

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction(services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(initialImplType, parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters) where TDecorator : class
            => ThenWrapWith<TDecorator>(null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWith<TDecorator>(Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, TDecorator> factoryFunction, params ITypedResolvable[] parameters) where TDecorator : class
        {
            AssertObjectTypeImplementsServiceType(typeof(TDecorator));

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction(wrapped, services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(typeof(TDecorator), parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWithType(Type decoratorType, params ITypedResolvable[] parameters)
            => ThenWrapWithType(decoratorType, null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWithType(Type decoratorType, Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunction, params ITypedResolvable[] parameters)
        {
            AssertObjectTypeImplementsServiceType(decoratorType);

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction(wrapped, services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(decoratorType, parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        void AssertObjectTypeImplementsServiceType(Type objectType)
        {
            if (objectType is null)
                throw new ArgumentNullException(nameof(objectType));
            if(!serviceType.IsAssignableFrom(objectType))
                throw new ArgumentException($"The type {objectType} must derive from {serviceType}.", nameof(objectType));
        }

        /// <summary>
        /// Initialises a new instance of <see cref="DecoratorBasedServiceResolutionInfoBuilder"/>.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="globalParameters">An optional collection of global parameters to be applied to every resolution.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceType"/> is <see langword="null" />.</exception>
        public DecoratorBasedServiceResolutionInfoBuilder(Type serviceType, params ITypedResolvable[] globalParameters)
        {
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            this.globalParameters = globalParameters;
            ResolutionInfo = new DecoratorBasedServiceResolutionInfo(serviceType);
        }
    }
}