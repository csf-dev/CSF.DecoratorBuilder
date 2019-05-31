public interface ICreatesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>()
        where TInitialImpl : TService;
}