using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsSingleObjectFromResolutionInfo"/> which uses functionality within
    /// <c>Microsoft.Extensions.DependencyInjection.Abstractions</c> to resolve services.
    /// </summary>
    public class DependencyInjectionObjectResolver : IGetsSingleObjectFromResolutionInfo
    {
        readonly IServiceProvider services;

        /// <inheritdoc/>
        public object GetService(Type serviceType, SingleObjectResolutionInfo resolutionInfo, object wrapped = null)
        {
            if (resolutionInfo is null)
                throw new ArgumentNullException(nameof(resolutionInfo));
            if(!(resolutionInfo.ResolutionFunction is null))
                return resolutionInfo.ResolutionFunction.Invoke(wrapped, services, resolutionInfo.Dependencies);

            var parameters = resolutionInfo.Dependencies.Select(x => x.Resolve(services)).ToList();

            if(wrapped != null)
                parameters.Insert(0, wrapped);
            
            return ActivatorUtilities.CreateInstance(services, resolutionInfo.Type, parameters.ToArray());
        }

        /// <summary>
        /// Initialises a new instance of <see cref="DependencyInjectionObjectResolver"/>.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public DependencyInjectionObjectResolver(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}