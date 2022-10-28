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
    public class GenericDecoratorBasedServiceResolutionInfoBuilder<TService> :  ICreatesDecorator<TService>,
                                                                                ICustomizesDecorator<TService>,
                                                                                IGetsDecoratorBasedServiceResolutionInfo
                                                                                where TService : class
    {
        readonly IEnumerable<ITypedResolvable> globalParameters;
        readonly DecoratorBasedServiceResolutionInfo resolutionInfo;

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>(params ITypedResolvable[] parameters) where TInitialImpl : class, TService
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TService), typeof(TInitialImpl), parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType, params ITypedResolvable[] parameters)
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TService), initialImplType, parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters) where TDecorator : class, TService
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TService), typeof(TDecorator), parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType, params ITypedResolvable[] parameters)
        {
            resolutionInfo.EnqueueServiceResolutionInfo(typeof(TService), decoratorType, parameters.Union(globalParameters));
            return this;
        }

        /// <inheritdoc/>
        public DecoratorBasedServiceResolutionInfo GetResolutionInfo() => resolutionInfo;

        /// <summary>
        /// Initialises a new instance of <see cref="GenericDecoratorBasedServiceResolutionInfoBuilder{TService}"/>.
        /// </summary>
        /// <param name="globalParameters">An optional collection of global parameters to be applied to every resolution.</param>
        public GenericDecoratorBasedServiceResolutionInfoBuilder(params ITypedResolvable[] globalParameters)
        {
            this.globalParameters = globalParameters;
            resolutionInfo = new DecoratorBasedServiceResolutionInfo(typeof(TService));
        }

    }
}