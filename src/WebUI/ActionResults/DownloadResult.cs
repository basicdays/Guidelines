using System.IO;
using System.Web.Mvc;

namespace Guidelines.WebUI.ActionResults
{
    public class DownloadResult : FilePathResult
    {
        public DownloadResult(string filePath)
            : base(filePath, GetMimeType(filePath))
        { }

        public DownloadResult(string filePath, string fileName)
            : base(filePath, GetMimeType(filePath))
        {
            FileDownloadName = fileName;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension != null)
            {
                string ext = fileExtension.ToLower();
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                {
                    mimeType = regKey.GetValue("Content Type").ToString();
                }
            }
            return mimeType;
        }
    }
}