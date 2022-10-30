using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

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
    public class AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter : IAutofacDecoratorBuilder
    {
        readonly DecoratorBasedServiceResolutionInfoBuilder wrapped;

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo ResolutionInfo => wrapped.ResolutionInfo;

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(params Parameter[] parameters) where TInitialImpl : class
        {
            wrapped.UsingInitialImpl<TInitialImpl>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType, params Parameter[] parameters)
        {
            wrapped.UsingInitialImplType(initialImplType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(params Parameter[] parameters) where TDecorator : class
        {
            wrapped.ThenWrapWith<TDecorator>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType, params Parameter[] parameters)
        {
            wrapped.ThenWrapWithType(decoratorType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(Func<IComponentContext, IEnumerable<Parameter>, TInitialImpl> factoryFunction,
                                                                          params Parameter[] parameters)
            where TInitialImpl : class
        {
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, TInitialImpl> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (serviceProvider, @params) => factoryFunction(((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                            @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.UsingInitialImpl<TInitialImpl>(factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType,
                                                                Func<IComponentContext, IEnumerable<Parameter>, object> factoryFunction,
                                                                params Parameter[] parameters)
        {
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (serviceProvider, @params) => factoryFunction(((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                            @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.UsingInitialImplType(initialImplType, factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(Func<object, IComponentContext, IEnumerable<Parameter>, TDecorator> factoryFunction,
                                                                    params Parameter[] parameters)
            where TDecorator : class
        {
            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, TDecorator> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, serviceProvider, @params) => factoryFunction(wrapped,
                                                                                     ((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                                     @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.ThenWrapWith<TDecorator>(factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType,
                                                            Func<object, IComponentContext, IEnumerable<Parameter>, object> factoryFunction,
                                                            params Parameter[] parameters)
        {
            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> factoryFunc = null;
            if(!(factoryFunction is null))
                factoryFunc = (wrapped, serviceProvider, @params) => factoryFunction(wrapped,
                                                                                     ((AutofacServiceProvider)serviceProvider).LifetimeScope,
                                                                                     @params.Cast<ParameterAdapter>().Select(x => x.Parameter).ToList());
            wrapped.ThenWrapWithType(decoratorType, factoryFunc, parameters.Select(x => new ParameterAdapter(x)).ToArray());
            return this;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter"/>.
        /// </summary>
        /// <param name="wrapped">The wrapped builder</param>
        /// <exception cref="ArgumentNullException">If <paramref name="wrapped"/> is <see langword="null" />.</exception>
        public AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter(DecoratorBasedServiceResolutionInfoBuilder wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}