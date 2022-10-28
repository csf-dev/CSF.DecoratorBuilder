using System;
using System.Collections.Generic;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A model for a series of <see cref="ServiceResolutionInfo"/> which describes 'a decorator stack'.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A service created using the decorator pattern is a series of service objects, each wrapping the
    /// one which precedes it.
    /// See https://en.wikipedia.org/wiki/Decorator_pattern for more info but a good analogy are Russian
    /// Matryoshka dolls, each doll contains another.
    /// Likewise, with services created using the decorator pattern, each object which implements the service
    /// interface wraps another object that implements the same interface.  Only one object in the overall service
    /// does not wrap an object of the same interface.
    /// </para>
    /// <para>
    /// To create a service that uses the decorator pattern a queue of services-to-resolve is specified.
    /// When resolving each of these services, that resolved object is passed as a dependency to the resolution
    /// of the next service in the queue.
    /// </para>
    /// </remarks>
    public class DecoratorBasedServiceResolutionInfo
    {
        /// <summary>
        /// Gets the type of the service to create using the decorator pattern.  This should generally be an interface or abstract class.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Gets a collection of specifications for each of the individual objects to resolve in order to create the overall service.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The first item in this queue must represent a 'plain' service type (which is not itself a decorator).
        /// All subsequent items must be decorator types.
        /// </para>
        /// </remarks>
        public Queue<ServiceResolutionInfo> ServicesToResolve { get; } = new Queue<ServiceResolutionInfo>();

        /// <summary>
        /// Creates a new instance of <see cref="ServiceResolutionInfo"/> from the specified parameters
        /// and enqueues it within <see cref="ServicesToResolve"/>.
        /// </summary>
        /// <param name="serviceType">The overall service type (typically an interface).</param>
        /// <param name="type">The type of the individual service to enqueue (typically a concrete class).</param>
        /// <param name="parameters">A collection of parameters for the resolution of <paramref name="type"/>.</param>
        public void EnqueueServiceResolutionInfo(Type serviceType, Type type, IEnumerable<ITypedResolvable> parameters)
        {
            var serviceInfo = GetServiceResolutionInfo(serviceType, type, parameters);
            ServicesToResolve.Enqueue(serviceInfo);
        }

        ServiceResolutionInfo GetServiceResolutionInfo(Type serviceType, Type type, IEnumerable<ITypedResolvable> parameters)
        {
            if (serviceType is null)
                throw new ArgumentNullException(nameof(serviceType));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if(!serviceType.IsAssignableFrom(type))
                throw new ArgumentException($"The type {type} must derive from {serviceType}.", nameof(type));

            var serviceInfo = new ServiceResolutionInfo(type);

            foreach(var param in parameters)
                serviceInfo.Dependencies.Add(param);

            return serviceInfo;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="DecoratorBasedServiceResolutionInfo"/>.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceType"/> is <see langword="null" />.</exception>
        public DecoratorBasedServiceResolutionInfo(Type serviceType)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
        }
    }
}