using System;

using DeserializedType = R5T.T0170.InstanceDescriptor;
using SerializedType = R5T.T0170.Serialization.InstanceDescriptor;


namespace R5T.T0170.Extensions
{
    public static class InstanceDescriptorExtensions
    {
        public static SerializedType ToSerializedType(this DeserializedType deserializedType)
        {
            return Instances.InstanceDescriptorOperator.ToSerializedType(deserializedType);
        }

        public static DeserializedType ToDeserializedType(this SerializedType serializedType)
        {
            return Instances.InstanceDescriptorOperator.ToDeserializedType(serializedType);
        }
    }
}
