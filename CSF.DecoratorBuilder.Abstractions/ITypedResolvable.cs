using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which can resolve a value of a specified type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Objects which implement this interface provide a rudimentary way to parameterise the creation of
    /// objects.  These may be either or both of the initial implementation or any of the decorators
    /// which participate in a service that uses the decorator pattern.
    /// </para>
    /// <para>
    /// The two implementations of this interface provided by this library are:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="TypedParam"/></term>
    /// <description>Provides a way to inject a value of a specified type.</description>
    /// </item>
    /// <item>
    /// <term><see cref="ResolvedType"/></term>
    /// <description>Provides a way to inject a resolved value of a specified type.</description>
    /// </item>
    /// </list>
    /// </remarks>
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