using System;
using Autofac.Core;

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
    public class AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter :   ICreatesAutofacDecorator,
                                                                              ICustomizesAutofacDecorator,
                                                                              IGetsDecoratorBasedServiceResolutionInfo
    {
        readonly DecoratorBasedServiceResolutionInfoBuilder wrapped;

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
        public DecoratorBasedServiceResolutionInfo GetResolutionInfo() => wrapped.GetResolutionInfo();

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