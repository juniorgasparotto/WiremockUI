using System;
using System.IO;

namespace WiremockUI
{
    public class FileModel
    {
        public string FullPath { get; private set; }
        public Exception LastException { get; private set; }

        private FileModel() { }

        public static FileModel Create(string fileName)
        {
            var fileModel = new FileModel()
            {
                FullPath = fileName
            };
            return fileModel;
        }

        public string GetOnlyFileName()
        {
            return Path.GetFileName(FullPath);
        }

        public string GetWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(FullPath);
        }

        public string GetContent(out Exception exception)
        {
            exception = null;
            try
            {
                return Helper.ReadAllText(FullPath);
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }
    }
}
