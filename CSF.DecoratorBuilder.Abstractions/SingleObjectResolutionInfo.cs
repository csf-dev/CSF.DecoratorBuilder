using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Model which represents information about how a service should be resolved.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This model differs from <see cref="DecoratorBasedServiceResolutionInfo"/> in that this class deals with
    /// only a single class within a 'decorator stack' (which may be made from many instances, wrapping one another).
    /// </para>
    /// </remarks>
    public class SingleObjectResolutionInfo
    {
        /// <summary>
        /// Gets the type of the service to be resolved.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets a collection of the dependencies for the current service instance.
        /// </summary>
        public IList<ITypedResolvable> Dependencies { get; } 

        /// <summary>
        /// Gets an optional custom function which should be used to resolve the service instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The parameters to this function are:
        /// </para>
        /// <list type="number">
        /// <item><description>The wrapped instance (to be decorated)</description></item>
        /// <item><description>A service provider, to use in the resolution of the service</description></item>
        /// <item><description>A collection of parameters for use in resolving the service</description></item>
        /// </list>
        /// <para>
        /// The function must return a non-null service instance.
        /// </para>
        /// </remarks>
        public Func<object,IServiceProvider,IEnumerable<ITypedResolvable>,object> ResolutionFunction { get; }

        /// <summary>
        /// Initialises a new instance of <see cref="SingleObjectResolutionInfo"/>.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="dependencies">An optional collection of dependencies for resolving this object.</param>
        /// <param name="resolutionFunction">An optional resolution function.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is <see langword="null" />.</exception>
        public SingleObjectResolutionInfo(Type type,
                                          IEnumerable<ITypedResolvable> dependencies = null,
                                          Func<object,IServiceProvider,IEnumerable<ITypedResolvable>,object> resolutionFunction = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Dependencies = dependencies?.ToList() ?? new List<ITypedResolvable>();
            ResolutionFunction = resolutionFunction;
        }
    }
}