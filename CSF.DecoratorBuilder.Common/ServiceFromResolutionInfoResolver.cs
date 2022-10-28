using System;
using System.Linq;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsDecoratedServiceFromResolutionInfo"/> which builds up the
    /// service using the decorator pattern.
    /// </summary>
    public class ServiceFromResolutionInfoResolver : IGetsDecoratedServiceFromResolutionInfo
    {
        readonly IGetsServiceFromServiceResolutionInfo serviceResolver;

        /// <inheritdoc/>
        public object GetDecoratedService(DecoratorBasedServiceResolutionInfo resolutionInfo)
        {
            if (resolutionInfo is null)
                throw new ArgumentNullException(nameof(resolutionInfo));
            if(!resolutionInfo.ServicesToResolve.Any())
                throw new ArgumentException($"The resolution info must contain at least one {nameof(ServiceResolutionInfo)}.", nameof(resolutionInfo));

            object impl = null;
            while(resolutionInfo.ServicesToResolve.Any())
            {
                var serviceInfo = resolutionInfo.ServicesToResolve.Dequeue();
                // Because we initialised the impl to null, on the first iteration the wrapped parameter will be null.
                // On subsequent iterations it will be "the result of the previous iteration".
                impl = serviceResolver.GetService(resolutionInfo.ServiceType, serviceInfo, wrapped: impl);
            }

            return impl;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceFromResolutionInfoResolver"/>.
        /// </summary>
        /// <param name="serviceResolver">A service resolver.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceResolver"/> is <see langword="null" />.</exception>
        public ServiceFromResolutionInfoResolver(IGetsServiceFromServiceResolutionInfo serviceResolver)
        {
            this.serviceResolver = serviceResolver ?? throw new ArgumentNullException(nameof(serviceResolver));
        }
    }
}