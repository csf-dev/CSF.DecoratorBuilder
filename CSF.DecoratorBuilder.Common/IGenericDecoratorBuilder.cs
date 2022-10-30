namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which implements all of <see cref="ICreatesDecorator{TService}"/>, <see cref="ICustomizesDecorator{TService}"/> &amp;
    /// <see cref="IHasDecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    /// <typeparam name="TService">The service type.</typeparam>
    public interface IGenericDecoratorBuilder<TService> : ICreatesDecorator<TService>, ICustomizesDecorator<TService>, IHasDecoratorBasedServiceResolutionInfo
        where TService : class {}
}