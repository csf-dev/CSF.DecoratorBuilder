using System;
using System.Collections.Generic;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    internal class AutofacParameterUtilities
    {
        internal IEnumerable<Parameter> AddTypedParam<TService>(TService impl, IEnumerable<Parameter> autofacParams)
        {

        }

        internal IEnumerable<Parameter> AddTypedParam(Type serviceType, object impl, IEnumerable<Parameter> autofacParams)
        {

        }
    }
}