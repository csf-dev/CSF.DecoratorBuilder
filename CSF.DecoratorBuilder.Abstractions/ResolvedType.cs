using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A model which represents an injectable dependency of a specified type, resolved from a service provider.
    /// </summary>
    public sealed class ResolvedType : ITypedResolvable
    {
        readonly Func<IServiceProvider, object> resolver;

        /// <inheritdoc/>
        public Type Type { get; }

        /// <inheritdoc/>
        public object Resolve(IServiceProvider services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            return resolver(services) ?? throw new ResolutionException("The resolved service must not be null.");
        } 

        /// <summary>
        /// Initialises a new instance of <see cref="ResolvedType"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <paramref name="resolver"/> is omitted or is <see langword="null" /> then the dependency will
        /// be directly resolved from the service provider object via <see cref="IServiceProvider.GetService(Type)"/>.
        /// </para>
        /// </remarks>
        /// <param name="type">The type of the dependency object.</param>
        /// <param name="resolver">An optional service-resolver function.</param>
        /// <exception cref="ArgumentNullException">If any constructor parameter is <see langword="null" />.</exception>
        public ResolvedType(Type type, Func<IServiceProvider,object> resolver = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            this.resolver = resolver ?? (s => s.GetService(Type));
        }
    }
}