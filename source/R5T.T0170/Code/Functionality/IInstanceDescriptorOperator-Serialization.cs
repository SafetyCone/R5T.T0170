using System;
using System.Linq;

using R5T.T0132;
using R5T.T0161.Extensions;
using R5T.T0162.Extensions;
using R5T.T0170.Extensions;
using R5T.T0171.Extensions;
using R5T.T0172.Extensions;
using R5T.T0173.Extensions;
using R5T.T0181;

using DeserializedType = R5T.T0170.InstanceDescriptor;
using SerializedType = R5T.T0170.Serialization.InstanceDescriptor;


namespace R5T.T0170
{
    public partial interface IInstanceDescriptorOperator : IFunctionalityMarker
    {
        public DeserializedType[] Deserialize_Synchronous(
            IJsonFilePath jsonFilePath)
        {
            var output = Instances.JsonOperator.Deserialize_Synchronous<SerializedType[]>(
                jsonFilePath.Value)
                .Select(x => x.ToDeserializedType())
                .Now();

            return output;
        }

        public void Serialize_Synchronous(
            IJsonFilePath jsonFilePath,
            DeserializedType[] instances)
        {
            var serializable = instances
                .Select(x => x.ToSerializedType())
                .Now();

            Instances.JsonOperator.Serialize_Synchronous(
                jsonFilePath.Value,
                serializable);
        }

        public SerializedType ToSerializedType(DeserializedType deserializedType)
        {
            var output = new SerializedType
            {
                DescriptionXml = deserializedType.DescriptionXml?.Value,
                IdentityName = deserializedType.IdentityName.Value,
                InstanceVarietyName = deserializedType.InstanceVarietyName.Value,
                IsObsolete = deserializedType.IsObsolete,
                KindMarkedFullMemberName = deserializedType.KindMarkedFullMemberName.Value,
                ProjectFilePath = deserializedType.ProjectFilePath.Value,
            };

            return output;
        }

        public DeserializedType ToDeserializedType(SerializedType serializedType)
        {
            var output = new DeserializedType
            {
                DescriptionXml = serializedType.DescriptionXml.ToDescriptionXml(),
                IdentityName = serializedType.IdentityName.ToIdentityName(),
                InstanceVarietyName = serializedType.InstanceVarietyName.ToInstanceVarietyName(),
                IsObsolete = serializedType.IsObsolete,
                KindMarkedFullMemberName = serializedType.KindMarkedFullMemberName.ToKindMarkedFullMemberName(),
                ProjectFilePath = serializedType.ProjectFilePath.ToProjectFilePath(),
            };

            return output;
        }
    }
}
