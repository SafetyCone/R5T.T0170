using System;
using System.Collections.Generic;


namespace R5T.T0170.N001
{
    /// <summary>
    /// Full <see cref="InstanceDescriptor"/> equality comparer using all properties.
    /// </summary>
    public class InstanceDescriptorEqualityComparer : IEqualityComparer<InstanceDescriptor>
    {
        #region Static

        public static InstanceDescriptorEqualityComparer Instance { get; } = new InstanceDescriptorEqualityComparer();

        #endregion


        public bool Equals(InstanceDescriptor x, InstanceDescriptor y)
        {
            var output = Instances.InstanceDescriptorOperator.Equals_Full(x, y);
            return output;
        }

        public int GetHashCode(InstanceDescriptor obj)
        {
            return Instances.InstanceDescriptorOperator.Get_HashCode(obj);
        }
    }
}


namespace R5T.T0170.N002
{
    /// <summary>
    /// An <see cref="InstanceDescriptor"/> equality comparer using all properties, except for description, for when we don't care if descriptions changed.
    /// </summary>
    public class InstanceDescriptorEqualityComparer : IEqualityComparer<InstanceDescriptor>
    {
        #region Static

        public static InstanceDescriptorEqualityComparer Instance { get; } = new InstanceDescriptorEqualityComparer();

        #endregion


        public bool Equals(InstanceDescriptor x, InstanceDescriptor y)
        {
            var output = Instances.InstanceDescriptorOperator.Equals_WithoutDescription(x, y);
            return output;
        }

        public int GetHashCode(InstanceDescriptor obj)
        {
            return Instances.InstanceDescriptorOperator.Get_HashCode(obj);
        }
    }
}
