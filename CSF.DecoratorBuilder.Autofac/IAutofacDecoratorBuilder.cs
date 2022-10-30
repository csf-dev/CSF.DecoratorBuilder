namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which implements all of <see cref="ICreatesAutofacDecorator"/>, <see cref="ICustomizesAutofacDecorator"/> &amp;
    /// <see cref="IHasDecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IAutofacDecoratorBuilder: ICreatesAutofacDecorator, ICustomizesAutofacDecorator, IHasDecoratorBasedServiceResolutionInfo {}
}