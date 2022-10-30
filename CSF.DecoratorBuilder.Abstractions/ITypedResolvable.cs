using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which can resolve a value of a specified type.
    /// </summary>
    public interface ITypedResolvable
    {
        /// <summary>
        /// Gets the type of the resolvable value.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Resolves the value, which should be a value of the type specified by <see cref="Type"/>.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <returns>The resolved value.</returns>
        object Resolve(IServiceProvider services);
    }
}