using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Tests
{
    public class TypedParameterComparer : IEqualityComparer<TypedParameter>, IEqualityComparer<Parameter>
    {
        public bool Equals(TypedParameter x, TypedParameter y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            return Equals(x.Type, y.Type) && Equals(x.Value, y.Value);
        }

        public int GetHashCode(TypedParameter obj)
        {
            if (obj is null) return 0;
            var typeHash = obj.Type?.GetHashCode() ?? 0;
            var valueHash = obj.Value?.GetHashCode() ?? 0;

            unchecked
            {
                return typeHash * 31 ^ valueHash;
            }
        }

        bool IEqualityComparer<Parameter>.Equals(Parameter x, Parameter y) => Equals(x as TypedParameter, y as TypedParameter);

        int IEqualityComparer<Parameter>.GetHashCode(Parameter obj) => GetHashCode(obj as TypedParameter);
    }
}
