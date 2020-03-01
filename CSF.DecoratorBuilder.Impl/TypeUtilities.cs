using System;
using System.Reflection;

namespace CSF.DecoratorBuilder
{
    internal static class TypeUtilities
    {
        internal static bool DoesImplTypeDeriveFromServiceType<TService>(Type implType)
        {
            return DoesImplTypeDeriveFromServiceType(implType, typeof(TService));
        }

        internal static bool DoesImplTypeDeriveFromServiceType(Type implType, Type serviceType)
        {
            if(implType == serviceType) return true;
            var implTypeInfo = implType.GetTypeInfo();
            var serviceTypeInfo = serviceType.GetTypeInfo();
            return serviceTypeInfo.IsAssignableFrom(implTypeInfo);
        }
    }
}