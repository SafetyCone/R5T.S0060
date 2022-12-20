using System;
using System.Collections.Generic;

using R5T.F0000;
using R5T.T0132;


namespace R5T.S0060.S003
{
    [FunctionalityMarker]
    public partial interface IFilePathProvider : IFunctionalityMarker
    {
        public string GetGmailAuthenticationFilePath()
        {
            var filePathsByMachineName = new Dictionary<string, string>
            {
                { MachineNames.Instance.Vanessa, D8S.Z0001.FilePaths.Instance.GmailAuthenticationFilePath }
            };

            var output = MachineNameOperator.Instance.GetValueByMachineName(
                filePathsByMachineName,
                () => F0002.PathOperator.Instance.GetFileName(
                    D8S.Z0001.FilePaths.Instance.GmailAuthenticationFilePath));

            return output;
        }
    }
}
