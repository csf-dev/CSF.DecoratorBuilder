using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A generic builder which creates an instance of <see cref="DecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances of this builder class must be used a maximum of once.  They are not reusable.
    /// </para>
    /// </remarks>
    /// <typeparam name="TService">The overall type of the service to be created.</typeparam>
    public class GenericDecoratorBasedServiceResolutionInfoBuilder<TService> : IGenericDecoratorBuilder<TService>
        where TService : class
    {
        readonly IEnumerable<ITypedResolvable> globalParameters;

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo ResolutionInfo { get; }

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>(params ITypedResolvable[] parameters) where TInitialImpl : class, TService
            => UsingInitialImpl<TInitialImpl>(null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>(Func<IServiceProvider, IEnumerable<ITypedResolvable>, TInitialImpl> factoryFunction,
                                                                             params ITypedResolvable[] parameters)
            where TInitialImpl : class, TService
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
        public ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType, params ITypedResolvable[] parameters)
            => UsingInitialImplType(initialImplType, null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType,
                                                                   Func<IServiceProvider, IEnumerable<ITypedResolvable>, TService> factoryFunction,
                                                                   params ITypedResolvable[] parameters)
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
        public ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters) where TDecorator : class, TService
            => ThenWrapWith<TDecorator>(null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(Func<TService, IServiceProvider, IEnumerable<ITypedResolvable>, TDecorator> factoryFunction,
                                                                       params ITypedResolvable[] parameters)
            where TDecorator : class, TService
        {
            AssertObjectTypeImplementsServiceType(typeof(TDecorator));

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction((TService) wrapped, services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(typeof(TDecorator), parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType, params ITypedResolvable[] parameters)
            => ThenWrapWithType(decoratorType, null, parameters);

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType,
                                                               Func<TService, IServiceProvider, IEnumerable<ITypedResolvable>, TService> factoryFunction,
                                                               params ITypedResolvable[] parameters)
        {
            AssertObjectTypeImplementsServiceType(decoratorType);

            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, services, @params) => factoryFunction((TService) wrapped, services, @params);
            
            var objectResolutionInfo = new SingleObjectResolutionInfo(decoratorType, parameters.Union(globalParameters), factoryFunc);
            ResolutionInfo.ServicesToResolve.Enqueue(objectResolutionInfo);
            return this;
        }

        static void AssertObjectTypeImplementsServiceType(Type objectType)
        {
            if (objectType is null)
                throw new ArgumentNullException(nameof(objectType));
            if (!typeof(TService).IsAssignableFrom(objectType))
                throw new ArgumentException($"The type {objectType} must derive from {typeof(TService)}.", nameof(objectType));
        }

        /// <summary>
        /// Initialises a new instance of <see cref="GenericDecoratorBasedServiceResolutionInfoBuilder{TService}"/>.
        /// </summary>
        /// <param name="globalParameters">An optional collection of global parameters to be applied to every resolution.</param>
        public GenericDecoratorBasedServiceResolutionInfoBuilder(params ITypedResolvable[] globalParameters)
        {
            this.globalParameters = globalParameters;
            ResolutionInfo = new DecoratorBasedServiceResolutionInfo(typeof(TService));
        }

    }
}