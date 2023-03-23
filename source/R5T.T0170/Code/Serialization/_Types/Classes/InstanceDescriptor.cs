using System;

using R5T.T0142;


namespace R5T.T0170.Serialization
{
    [DataTypeMarker]
    public class InstanceDescriptor
    {
        public string InstanceVarietyName { get; set; }
        public string IdentityName { get; set; }
        public string KindMarkedFullMemberName { get; set; }
        public string ProjectFilePath { get; set; }
        public string DescriptionXml { get; set; }
        public bool IsObsolete { get; set; }
    }
}
