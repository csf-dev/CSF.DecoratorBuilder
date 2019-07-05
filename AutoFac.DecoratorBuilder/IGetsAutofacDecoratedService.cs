﻿using System;

namespace CSF.DecoratorBuilder.AutoFac
{
    public interface IGetsAutofacDecoratedService
    {
        TService GetDecoratedService<TService>(Func<ICreatesAutofacDecorator<TService>,ICustomizesAutofacDecorator<TService>> customizationFunc)
            where TService : class;

        object GetDecoratedService(Type serviceType, Func<ICreatesAutofacDecorator,ICustomizesAutofacDecorator> customizationFunc);
    }
}