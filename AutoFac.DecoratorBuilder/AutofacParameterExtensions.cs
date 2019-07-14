using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    internal static class AutofacParameterExtensions
    {
        static internal IEnumerable<Parameter> AddTypedParam<TService>(this IEnumerable<Parameter> autofacParams, TService impl)
        {
            var newParam = TypedParameter.From(impl);
            return (autofacParams ?? Enumerable.Empty<Parameter>()).Union(new[] { newParam });
        }

        static internal IEnumerable<Parameter> AddTypedParam(this IEnumerable<Parameter> autofacParams, Type serviceType, object impl)
        {
            var newParam = new TypedParameter(serviceType, impl);
            return (autofacParams ?? Enumerable.Empty<Parameter>()).Union(new[] { newParam });
        }
    }
}