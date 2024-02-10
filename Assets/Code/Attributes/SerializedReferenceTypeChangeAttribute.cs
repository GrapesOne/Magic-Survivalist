using System;
using NaughtyAttributes;

namespace Code.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SerializedReferenceTypeChangeAttribute : DrawerAttribute
    {
        public Type ArrayType;
        public SerializedReferenceTypeChangeAttribute(Type arrayType)
        {
            ArrayType = arrayType;
        }
    }
}
