using System;


namespace R5T.S0060.S003
{
    public class MachineNames : IMachineNames
    {
        #region Infrastructure

        public static IMachineNames Instance { get; } = new MachineNames();


        private MachineNames()
        {
        }

        #endregion
    }
}
