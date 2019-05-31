public interface IGetsDecoratedService
{
    TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc)
        where TService : class;
}