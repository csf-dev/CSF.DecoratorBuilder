namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which implements all of <see cref="ICreatesAutofacDecorator{TService}"/>, <see cref="ICustomizesAutofacDecorator{TService}"/> &amp;
    /// <see cref="IHasDecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IAutofacGenericDecoratorBuilder<TService> : ICreatesAutofacDecorator<TService>,
                                                                 ICustomizesAutofacDecorator<TService>,
                                                                 IHasDecoratorBasedServiceResolutionInfo
        where TService : class {}
}