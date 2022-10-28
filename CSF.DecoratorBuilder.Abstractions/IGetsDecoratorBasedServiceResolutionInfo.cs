namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which can get <see cref="DecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IGetsDecoratorBasedServiceResolutionInfo
    {
        /// <summary>
        /// Gets the resolution information for the decorator-based services.
        /// </summary>
        /// <returns>Resolution information.</returns>
        DecoratorBasedServiceResolutionInfo GetResolutionInfo();
    }
}