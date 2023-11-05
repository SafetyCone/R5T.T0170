using System;

using R5T.L0062.T000;
using R5T.L0063.T000;
using R5T.T0142;
using R5T.T0171;
using R5T.T0172;
using R5T.T0173;


namespace R5T.T0170
{
    [DataTypeMarker]
    public class InstanceDescriptor
    {
        public IInstanceVarietyName InstanceVarietyName { get; set; }
        public IIdentityString IdentityString { get; set; }
        public ISignatureString SignatureString { get; set; }
        public IProjectFilePath ProjectFilePath { get; set; }
        public IDescriptionXml DescriptionXml { get; set; }
        public bool IsObsolete { get; set; }


        public override string ToString()
        {
            var representation = this.SignatureString.ToString();
            return representation;
        }
    }
}
