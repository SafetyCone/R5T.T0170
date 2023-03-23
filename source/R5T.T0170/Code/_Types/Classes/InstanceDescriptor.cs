using System;

using R5T.T0142;
using R5T.T0161;
using R5T.T0162;
using R5T.T0171;
using R5T.T0172;
using R5T.T0173;


namespace R5T.T0170
{
    [DataTypeMarker]
    public class InstanceDescriptor
    {
        public InstanceVarietyName InstanceVarietyName { get; set; }
        public IdentityName IdentityName { get; set; }
        public KindMarkedFullMemberName KindMarkedFullMemberName { get; set; }
        public IProjectFilePath ProjectFilePath { get; set; }
        public DescriptionXml DescriptionXml { get; set; }
        public bool IsObsolete { get; set; }


        public override string ToString()
        {
            var representation = this.KindMarkedFullMemberName.ToString();
            return representation;
        }
    }
}