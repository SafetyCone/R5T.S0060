using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.L0029;
using R5T.T0132;
using Renci.SshNet;

namespace R5T.S0060.Deploy
{
    [FunctionalityMarker]
    public partial interface IDeployScripts : IFunctionalityMarker
    {
        public async Task DeployToLocalCloudBinariesDirectory()
        {
            /// Inputs.
            var targetProjectName =
                //"R5T.S0060.S001"
                //"R5T.S0060.S002"
                "R5T.S0060.S003"
                ;
            var localCloudBinariesDirectoryPath = Instances.DirectoryPaths.LocalCloudBinariesDirectoryPath;


            /// Run.
            var targetProjectFilePath = Instances.ProjectPathConventions.GetExecutableSiblingProjectFilePath(targetProjectName);

            var currentBinariesDirectoryPath = await Instances.LoggingOperator.InConsoleLoggerContext(
                nameof(DeployToLocalCloudBinariesDirectory),
                async logger =>
                {
                    var currentBinariesDirectoryPath = await Instances.PublicationOperator.Publish(
                        targetProjectFilePath,
                        localCloudBinariesDirectoryPath,
                        logger);

                    return currentBinariesDirectoryPath;
                });

            Instances.WindowsExplorerOperator.OpenDirectoryInExplorer(currentBinariesDirectoryPath);
        }

        /// <summary>
        /// Requires <see cref="DeployToLocalCloudBinariesDirectory"></see> to be run first.
        /// </summary>
        public async Task DeployToRemoteBinariesDirectory()
        {
            /// Inputs.
            var targetProjectName =
                //"R5T.S0060.S001"
                //"R5T.S0060.S002"
                "R5T.S0060.S003"
                ;
            var remoteServerFriendlyName = "TechnicalBlog";
            var remoteDeployDirectoryPath = $@"/var/www/{targetProjectName}";

            var archiveFileName = @"Archive.zip";
            var localTemporaryDirectoryPath = @"C:\Temp";
            var remoteTemporaryDirectoryPath = @"/home/ec2-user";
            var localCloudBinariesDirectoryPath = Instances.DirectoryPaths.LocalCloudBinariesDirectoryPath;


            /// Run.
            IEnumerable<Func<RemoteDeployContext, SshClient, Task>> extras = targetProjectName switch
            {
                "R5T.S0060.S003" => EnumerableOperator.Instance.From(D8S.F0001.DeployOperations.Instance.CopyGmailAuthentication),
                _ => RemoteDeployActions.Instance.NoActions(),
            };

            var awsRemoteServerAuthentication = Instances.AwsAuthenticationOperator.GetRemoteServerAuthentication(
                Instances.FilePaths.AwsRemoteServerConfigurationJsonFilePath,
                remoteServerFriendlyName);

            var targetProjectFilePath = Instances.ProjectPathConventions.GetExecutableSiblingProjectFilePath(targetProjectName);

            var currentBinariesDirectoryPath = Instances.PublicationPathsOperator.GetCurrentBinariesOutputDirectoryPath(
                localCloudBinariesDirectoryPath,
                targetProjectFilePath);

            var remoteDeployContext = new RemoteDeployContext
            {
                ArchiveFileName = archiveFileName,
                DestinationRemoteBinariesDirectoryPath = remoteDeployDirectoryPath,
                LocalTemporaryDirectoryPath = localTemporaryDirectoryPath,
                RemoteTemporaryDirectoryPath = remoteTemporaryDirectoryPath,
                SourceLocalBinariesDirectoryPath = currentBinariesDirectoryPath,
            };

            await Instances.LoggingOperator.InConsoleLoggerContext(
                nameof(DeployToLocalCloudBinariesDirectory),
                async logger =>
                {
                    await DeployOperator.Instance.DeployToRemote(
                        awsRemoteServerAuthentication,
                        remoteDeployContext,
                        EnumerableOperator.Instance.From(
                            RemoteDeployActions.Instance.None),
                        EnumerableOperator.Instance.From(
                            RemoteDeployActions.Instance.ChangePermissionsOnRemoteDirectory(logger))
                            .AppendRange(extras),
                        logger);
                });
        }
    }
}
