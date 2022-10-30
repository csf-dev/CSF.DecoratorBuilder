using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

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
    public class AutofacGenericDecoratorBasedServiceResolutionInfoBuilderAdapter<TService> : IAutofacGenericDecoratorBuilder<TService> where TService : class
    {
        readonly GenericDecoratorBasedServiceResolutionInfoBuilder<TService> wrapped;

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo ResolutionInfo => wrapped.ResolutionInfo;

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] parameters) where TInitialImpl : class, TService
        {
            wrapped.UsingInitialImpl<TInitialImpl>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(Func<IComponentContext, IEnumerable<Parameter>, TInitialImpl> factoryFunction,
                                                                                    params Parameter[] parameters)
            where TInitialImpl : class, TService
        {
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, TInitialImpl> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (serviceProvider, @params) => factoryFunction(((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                            @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.UsingInitialImpl<TInitialImpl>(factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] parameters)
        {
            wrapped.UsingInitialImplType(initialImplType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType,
                                                                          Func<IComponentContext, IEnumerable<Parameter>, TService> factoryFunction,
                                                                          params Parameter[] parameters)
        {
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, TService> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (serviceProvider, @params) => factoryFunction(((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                            @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.UsingInitialImplType(initialImplType, factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] parameters) where TDecorator : class, TService
        {
            wrapped.ThenWrapWith<TDecorator>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(Func<TService, IComponentContext, IEnumerable<Parameter>, TDecorator> factoryFunction,
                                                                              params Parameter[] parameters)
            where TDecorator : class, TService
        {
            Func<TService, IServiceProvider, IEnumerable<ITypedResolvable>, TDecorator> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, serviceProvider, @params) => factoryFunction(wrapped,
                                                                                     ((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                                     @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.ThenWrapWith<TDecorator>(factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] parameters)
        {
            wrapped.ThenWrapWithType(decoratorType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType,
                                                                      Func<TService, IComponentContext, IEnumerable<Parameter>, TService> factoryFunction,
                                                                      params Parameter[] parameters)
        {
            Func<TService, IServiceProvider, IEnumerable<ITypedResolvable>, TService> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, serviceProvider, @params) => factoryFunction(wrapped,
                                                                                     ((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                                     @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.ThenWrapWithType(decoratorType, factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="AutofacGenericDecoratorBasedServiceResolutionInfoBuilderAdapter{TService}"/>.
        /// </summary>
        /// <param name="wrapped">The wrapped builder</param>
        /// <exception cref="ArgumentNullException">If <paramref name="wrapped"/> is <see langword="null" />.</exception>
        public AutofacGenericDecoratorBasedServiceResolutionInfoBuilderAdapter(GenericDecoratorBasedServiceResolutionInfoBuilder<TService> wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}