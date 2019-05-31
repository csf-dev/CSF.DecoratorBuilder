public interface ICustomizesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> ThenWrapWith<TDecorator>()
        where TDecorator: TService;
}