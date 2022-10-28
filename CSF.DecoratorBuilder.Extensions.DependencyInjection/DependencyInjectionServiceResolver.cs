using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsServiceFromServiceResolutionInfo"/> which uses functionality within
    /// <c>Microsoft.Extensions.DependencyInjection.Abstractions</c> to resolve services.
    /// </summary>
    public class DependencyInjectionServiceResolver : IGetsServiceFromServiceResolutionInfo
    {
        readonly IServiceProvider services;

        /// <inheritdoc/>
        public object GetService(Type serviceType, ServiceResolutionInfo resolutionInfo, object wrapped = null)
        {
            var parameters = resolutionInfo.Dependencies.Select(x => x.Resolve(services)).ToList();
            if(wrapped != null)
                parameters.Insert(0, wrapped);
            
            return ActivatorUtilities.CreateInstance(services, resolutionInfo.Type, parameters.ToArray());
        }

        /// <summary>
        /// Initialises a new instance of <see cref="DependencyInjectionServiceResolver"/>.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public DependencyInjectionServiceResolver(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}