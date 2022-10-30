using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which resolves an instance of a single object, from a <see cref="SingleObjectResolutionInfo"/>.
    /// </summary>
    public interface IGetsSingleObjectFromResolutionInfo
    {
        /// <summary>
        /// Resolve an instance of the object.
        /// </summary>
        /// <param name="serviceType">The overall service type.</param>
        /// <param name="resolutionInfo">The resolution information for the object to resolve.</param>
        /// <param name="wrapped">An optional instance of a service to wrap.</param>
        /// <returns>The resolved object.</returns>
        object GetService(Type serviceType, SingleObjectResolutionInfo resolutionInfo, object wrapped = null);
    }
}