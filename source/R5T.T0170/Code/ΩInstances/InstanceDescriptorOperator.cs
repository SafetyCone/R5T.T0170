using System;


namespace R5T.T0170
{
    public class InstanceDescriptorOperator : IInstanceDescriptorOperator
    {
        #region Infrastructure

        public static IInstanceDescriptorOperator Instance { get; } = new InstanceDescriptorOperator();


        private InstanceDescriptorOperator()
        {
        }

        #endregion
    }
}
