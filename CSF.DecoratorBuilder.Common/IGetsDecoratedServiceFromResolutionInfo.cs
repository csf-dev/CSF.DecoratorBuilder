namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which resolves a service using the decorator pattern, based upon a <see cref="DecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IGetsDecoratedServiceFromResolutionInfo
    {
        /// <summary>
        /// Get the service, resolved using the decorator pattern.
        /// </summary>
        /// <param name="resolutionInfo">Information about how the service should be resolved.</param>
        /// <returns>The resolved service.</returns>
        object GetDecoratedService(DecoratorBasedServiceResolutionInfo resolutionInfo);
    }
}