using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class AutofacDecoratorBuilder : ICreatesAutofacDecorator
    {
        public ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : class
        {
            throw new NotImplementedException();
        }

        public ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            throw new NotImplementedException();
        }
    }
}