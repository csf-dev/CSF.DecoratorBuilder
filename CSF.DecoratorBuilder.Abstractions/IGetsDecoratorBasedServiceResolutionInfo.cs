namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which can get <see cref="DecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IHasDecoratorBasedServiceResolutionInfo
    {
        /// <summary>
        /// Gets the resolution information for the decorator-based services.
        /// </summary>
        DecoratorBasedServiceResolutionInfo ResolutionInfo { get; }
    }
}