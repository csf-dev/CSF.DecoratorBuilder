using System;
using System.Reflection;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A model which represents an injectable dependency of a specified type, created from a specific value.
    /// </summary>
    public sealed class TypedParam : ITypedResolvable
    {
        readonly object value;

        /// <inheritdoc/>
        public Type Type { get; }

        /// <inheritdoc/>
        public object Resolve(IServiceProvider services) => value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedParam"/> class.
        /// </summary>
        /// <param name="type">The type of the dependency object.</param>
        /// <param name="value">The dependency object.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If <paramref name="value"/> is non-<see langword="null" /> but does not derive from <paramref name="type"/>.</exception>
        public TypedParam(Type type, object value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));

            if(!(value is null) && !type.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                throw new ArgumentException($"The value type {value.GetType()} must derive from {type}.", nameof(value));
            
            this.value = value;
        }

        /// <summary>
        /// Convenience function to create a new instance of <see cref="TypedParam"/> from an object instance.
        /// </summary>
        /// <returns>The typed parameter.</returns>
        /// <param name="value">The dependency object.</param>
        /// <typeparam name="T">The type of the dependency object.</typeparam>
        public static TypedParam From<T>(T value) => new TypedParam(typeof(T), value);
    }
}
