using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An implementation of <see cref="ITypedResolvable"/> which wraps an Autofac <see cref="Autofac.Core.Parameter"/>.
    /// </summary>
    public class ParameterAdapter : ITypedResolvable
    {
        /// <summary>
        /// Gets the wrapped parameter.
        /// </summary>
        public Parameter Parameter { get; }

        /// <summary>
        /// Always returns the type <see cref="System.Object"/>.
        /// </summary>
        public Type Type => typeof(object);

        /// <summary>
        /// Always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="services">A service provider.</param>
        /// <returns>Not applicable, this method always raises an exception.</returns>
        /// <exception cref="NotSupportedException">This method always throws this exception, whilst this is a Liskov
        /// Substitution violation, this class is specifically detected and this method is never used.</exception>
        public object Resolve(IServiceProvider services)
            => throw new NotSupportedException($"This implementation of {nameof(ITypedResolvable)} does not support the {nameof(Resolve)} method.");

        /// <summary>
        /// Initialises a new instance of <see cref="ParameterAdapter"/>.
        /// </summary>
        /// <param name="parameter">The autofac parameter.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="parameter"/> is <see langword="null" />.</exception>
        public ParameterAdapter(Parameter parameter)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }
    }
}