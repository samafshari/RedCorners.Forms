using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RedCorners.Forms.Extensions
{
    public static class ResourceExtensions
    {
        public static string SharedFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string GetRandomFileName(string prefix = "", string suffix = "")
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = $"{prefix}{guid}{suffix}";
            return fileName;
        }

        public static string GetSharedFilePath(string fileName = null)
        {
            var path = SharedFolderPath;
            if (!string.IsNullOrWhiteSpace(fileName))
                path = Path.Combine(path, fileName);
            return path;
        }

        public static string GetRandomFilePath(string prefix = "", string suffix = "")
        {
            return GetSharedFilePath(GetRandomFileName(prefix, suffix));
        }

        public static async Task SaveStreamAsync(Stream stream, string path)
        {
            using (var fileStream = File.Create(path))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }
        }

        public static void SaveStream(Stream stream, string path)
        {
            using (var fileStream = File.Create(path))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
