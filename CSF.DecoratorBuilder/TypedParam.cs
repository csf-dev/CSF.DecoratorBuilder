using System;
namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An abstraction for an injected object of a specified type.  This is equivalent to Autofac's
    /// <c>TypedParameter</c> object.  This will will be mapped to an Autofac typed parameter when used
    /// with an Autofac container.
    /// </summary>
    public sealed class TypedParam
    {
        /// <summary>
        /// Gets the dependency type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; }

        /// <summary>
        /// Gets the dependency object.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedParam"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="value">Value.</param>
        public TypedParam(Type type, object value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Value = value;
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
