using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsDecoratedServiceFromResolutionInfo"/> which makes use of the functionality
    /// within an <see cref="IComponentContext"/> to resolve services.
    /// </summary>
    public class AutofacServiceResolver : IGetsSingleObjectFromResolutionInfo
    {
        readonly ILifetimeScope scope;
        readonly IServiceProvider services;

        /// <inheritdoc/>
        public object GetService(Type serviceType, SingleObjectResolutionInfo resolutionInfo, object wrapped = null)
        {
            if (resolutionInfo is null)
                throw new ArgumentNullException(nameof(resolutionInfo));
            if(!(resolutionInfo.ResolutionFunction is null))
                return resolutionInfo.ResolutionFunction.Invoke(wrapped, services, resolutionInfo.Dependencies);

            var parameters = resolutionInfo.Dependencies.Select(GetParameter).ToList();
            if(wrapped != null)
                parameters.Insert(0, new TypedParameter(serviceType, wrapped));

            return scope.Resolve(resolutionInfo.Type, parameters);
        }

        Parameter GetParameter(ITypedResolvable resolvable)
            => resolvable is ParameterAdapter parameterAdapter
                    ? parameterAdapter.Parameter
                    : new TypedParameter(resolvable.Type, resolvable.Resolve(services));

        /// <summary>
        /// Initialises a new instance of <see cref="AutofacServiceResolver"/>.
        /// </summary>
        /// <param name="scope">An autofac lifetime scope.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="scope"/> is <see langword="null" />.</exception>
        public AutofacServiceResolver(ILifetimeScope scope)
        {
            this.scope = scope ?? throw new System.ArgumentNullException(nameof(scope));
            this.services = new AutofacServiceProvider(scope);
        }
    }
}