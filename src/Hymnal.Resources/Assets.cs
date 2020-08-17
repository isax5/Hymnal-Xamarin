using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hymnal.Resources
{
    public class Assets : IAssets
    {
        private static IAssets current;
        public static IAssets Current
        {
            get
            {
                if (current == null)
                    current = new Assets();

                return current;
            }
        }

        private Assets()
        { }

        public Stream GetResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames
                .Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (!resourcePaths.Any())
            {
                throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));
            }

            if (resourcePaths.Count() > 1)
            {
                throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
            }

            return assembly.GetManifestResourceStream(resourcePaths.Single());
        }

        public string GetResourceString(Assembly assembly, string resourceFileName)
        {
            Stream stream = GetResourceStream(assembly, resourceFileName);

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
        public string GetResourceString(string resourceFileName)
        {
            return GetResourceString(Assembly.GetExecutingAssembly(), resourceFileName);
        }
    }
}
