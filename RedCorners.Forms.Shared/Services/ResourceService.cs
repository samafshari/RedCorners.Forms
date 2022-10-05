using RedCorners.Forms.Services;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using RedCorners.Forms.Extensions;

[assembly: Dependency(typeof(ResourceService))]
namespace RedCorners.Forms.Services
{
    public class ResourceService
    {
        readonly Dictionary<(Assembly, string), string> resourceNames = new Dictionary<(Assembly, string), string>();
        readonly Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

        public Assembly GetAssembly()
        {
            return GetType().Assembly;
        }

        public Assembly GetAssembly(string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            if (!assemblies.ContainsKey(query))
                assemblies[query] = AppDomain.CurrentDomain.GetAssemblies()
                    .OrderBy(x => x.GetName().Name.Length)
                    .FirstOrDefault(x => x.GetName().Name.Contains(query));

            return assemblies[query];
        }

        public string FindResourceName(string query, Assembly assembly)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            if (!resourceNames.ContainsKey((assembly, query)))
                resourceNames[(assembly, query)] = assembly
                    .GetManifestResourceNames()
                    .OrderBy(x => x.Length)
                    .FirstOrDefault(x => x.Contains(query));

            return resourceNames[(assembly, query)];
        }

        public Stream GetResourceStream(string query, Assembly assembly)
        {
            var name = FindResourceName(query, assembly);
            return assembly.GetManifestResourceStream(name);
        }

        public string GetTextResourceStream(string query, Assembly assembly)
        {
            using (var stream = GetResourceStream(query, assembly))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public string GetRandomFileName(string prefix = "", string suffix = "") =>
            ResourceExtensions.GetRandomFileName(prefix, suffix);

        public string GetSharedFilePath(string fileName = null) =>
            ResourceExtensions.GetSharedFilePath(fileName);

        public string GetRandomFilePath(string prefix = "", string suffix = "") =>
            ResourceExtensions.GetRandomFilePath(prefix, suffix);

        public Task SaveStreamAsync(Stream stream, string path) =>
            ResourceExtensions.SaveStreamAsync(stream, path);

        public void SaveStream(Stream stream, string path) =>
            ResourceExtensions.SaveStream(stream, path);
    }
}