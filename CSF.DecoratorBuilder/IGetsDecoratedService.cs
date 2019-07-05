using System;

namespace CSF.DecoratorBuilder
{
    public interface IGetsDecoratedService
    {
        TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc)
            where TService : class;

        object GetDecoratedService(Type serviceType, Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc);
    }
}