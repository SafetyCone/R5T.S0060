using System;


namespace R5T.S0060.S003
{
    public class FilePathProvider : IFilePathProvider
    {
        #region Infrastructure

        public static IFilePathProvider Instance { get; } = new FilePathProvider();


        private FilePathProvider()
        {
        }

        #endregion
    }
}
