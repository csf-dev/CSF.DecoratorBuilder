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
    public class DecoratorBasedServiceResolutionInfoBuilder :   ICreatesDecorator,
                                                                ICustomizesDecorator,
                                                                IGetsDecoratorBasedServiceResolutionInfo
    {
        readonly Type serviceType;
        readonly IEnumerable<ITypedResolvable> globalParameters;
        readonly DecoratorBasedServiceResolutionInfo resolutionInfo;

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImpl<TInitialImpl>(params ITypedResolvable[] parameters) where TInitialImpl : class
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TInitialImpl), parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator UsingInitialImplType(Type initialImplType, params ITypedResolvable[] parameters)
        {
            resolutionInfo.EnqueueServiceResolutionInfo(initialImplType, parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters) where TDecorator : class
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TDecorator), parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator ThenWrapWithType(Type decoratorType, params ITypedResolvable[] parameters)
        {
            resolutionInfo.EnqueueServiceResolutionInfo(decoratorType, parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo GetResolutionInfo() => resolutionInfo;

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
            resolutionInfo = new DecoratorBasedServiceResolutionInfo(serviceType);
        }
    }
}