using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Exception thrown when there is an unexpected exception resolving a service.
    /// </summary>
    [Serializable]
    public class ResolutionException : Exception
    {
        /// <summary>
        /// Initialises a new instance of <see cref="ResolutionException"/>.
        /// </summary>
        public ResolutionException() {}

        /// <summary>
        /// Initialises a new instance of <see cref="ResolutionException"/>.
        /// </summary>
        /// <param name="message">An exception message.</param>
        public ResolutionException(string message) : base(message) {}

        /// <summary>
        /// Initialises a new instance of <see cref="ResolutionException"/>.
        /// </summary>
        /// <param name="message">An exception message.</param>
        /// <param name="inner">An inner exception.</param>
        public ResolutionException(string message, System.Exception inner) : base(message, inner) {}

        /// <summary>
        /// Initialises a new instance of <see cref="ResolutionException"/>.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ResolutionException(System.Runtime.Serialization.SerializationInfo info,
                                      System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
    }
}