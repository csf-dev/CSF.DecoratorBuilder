namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An object which implements all of <see cref="ICreatesDecorator"/>, <see cref="ICustomizesDecorator"/> &amp;
    /// <see cref="IHasDecoratorBasedServiceResolutionInfo"/>.
    /// </summary>
    public interface IDecoratorBuilder : ICreatesDecorator, ICustomizesDecorator, IHasDecoratorBasedServiceResolutionInfo {}
}