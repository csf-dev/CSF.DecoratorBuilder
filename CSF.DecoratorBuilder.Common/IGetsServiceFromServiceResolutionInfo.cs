using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which resolves an instance of a single service, from a <see cref="ServiceResolutionInfo"/>.
    /// </summary>
    public interface IGetsServiceFromServiceResolutionInfo
    {
        /// <summary>
        /// Resolve an instance of the service.
        /// </summary>
        /// <param name="serviceType">The overall service type.</param>
        /// <param name="resolutionInfo">The resolution information for the service.</param>
        /// <param name="wrapped">An optional instance of a service to wrap.</param>
        /// <returns>The resolved service.</returns>
        object GetService(Type serviceType, ServiceResolutionInfo resolutionInfo, object wrapped = null);
    }
}