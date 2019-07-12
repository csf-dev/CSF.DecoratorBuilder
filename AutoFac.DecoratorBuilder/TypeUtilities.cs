using System;
using System.Reflection;

namespace CSF.DecoratorBuilder.AutoFac
{
    internal class TypeUtilities
    {
        internal static bool DoesImplTypeDeriveFromServiceType<TService>(Type implType)
        {
            return DoesImplTypeDeriveFromServiceType(implType, typeof(TService));
        }

        internal static bool DoesImplTypeDeriveFromServiceType(Type implType, Type serviceType)
        {
            return implType == serviceType || implType.GetTypeInfo().IsSubclassOf(serviceType);
        }
    }
}