public interface ICustomizesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> ThenWrapWith<TDecorator>()
        where TDecorator : TService;
}

public interface ICustomizesDecorator
{
    ICustomizesDecorator ThenWrapWith<TDecorator>()
        where TDecorator : class;

    ICustomizesDecorator ThenWrapWithType(Type decoratorType);
}