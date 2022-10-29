using System;
using Autofac.Core;

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
    public class AutofacGenericDecoratorBasedServiceResolutionInfoBuilderAdapter<TService> :  ICreatesAutofacDecorator<TService>,
                                                                                              ICustomizesAutofacDecorator<TService>,
                                                                                              IGetsDecoratorBasedServiceResolutionInfo
        where TService : class
    {
        readonly GenericDecoratorBasedServiceResolutionInfoBuilder<TService> wrapped;

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] parameters) where TInitialImpl : class, TService
        {
            wrapped.UsingInitialImpl<TInitialImpl>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] parameters)
        {
            wrapped.UsingInitialImplType(initialImplType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] parameters) where TDecorator : class, TService
        {
            wrapped.ThenWrapWith<TDecorator>(parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] parameters)
        {
            wrapped.ThenWrapWithType(decoratorType, parameters.ToTypedResolvables());
            return this;
        }

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo GetResolutionInfo() => wrapped.GetResolutionInfo();

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