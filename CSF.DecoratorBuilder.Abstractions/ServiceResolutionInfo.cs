using System;
using System.Collections.Generic;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Model which represents information about how a service should be resolved.
    /// </summary>
    public class ServiceResolutionInfo
    {
        /// <summary>
        /// Gets the type of the service to be resolved.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets a collection of the dependencies for the current service instance.
        /// </summary>
        public IList<ITypedResolvable> Dependencies { get; } = new List<ITypedResolvable>();

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceResolutionInfo"/>.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is <see langword="null" />.</exception>
        public ServiceResolutionInfo(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}