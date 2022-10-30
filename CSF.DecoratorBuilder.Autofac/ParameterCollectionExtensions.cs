using System;
using System.Collections.Generic;
using System.Linq;
using CSF.DecoratorBuilder;

namespace Autofac.Core
{
    /// <summary>
    /// Extension methods for collections of Autofac parameters.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This functionality is intended to make it easier to use collections of <see cref="Parameter"/> with decorator builder types.
    /// </para>
    /// </remarks>
    public static class ParameterCollectionExtensions
    {
        /// <summary>
        /// Converts a collection of <see cref="Parameter"/> to an array of <see cref="ITypedResolvable"/>, for use
        /// in the decorator builder logic.
        /// </summary>
        /// <param name="autofacParams">A collection of Autofac parameters.</param>
        /// <returns>An array of typed resolvables.</returns>
        public static ITypedResolvable[] ToTypedResolvables(this IEnumerable<Parameter> autofacParams)
            => autofacParams?.Select(x => new ParameterAdapter(x)).ToArray() ?? Array.Empty<ITypedResolvable>();
    }
}