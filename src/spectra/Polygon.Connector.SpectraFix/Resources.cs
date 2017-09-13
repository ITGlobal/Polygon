using System;
using System.IO;
using System.Reflection;

namespace Polygon.Connector.SpectraFix
{
    internal static class Resources
    {
        public static string FIX44 { get; private set; }

        public static void DeployDataDictionaries()
        {
            FIX44 = DeployResource("FIX44.xml");
        }

        private static string DeployResource(string resourceName)
        {
            resourceName = $"Polygon.Connector.SpectraFix.Resources.{resourceName}";

            var path = Path.Combine(Path.GetTempPath(), "Polygon.Connector.SpectraFix", resourceName);
            if (!TryDeployTo(resourceName, path))
            {
                path = Path.GetTempFileName();
                if (!TryDeployTo(resourceName, path))
                {
                    throw new Exception($"Failed to write file \"{path}\"");
                }
            }

            return path;
        }

        private static bool TryDeployTo(string resourceName, string path)
        {
            using (var resource = typeof(Resources).GetTypeInfo()
                .Assembly.GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    throw new Exception($"Resource \"{resourceName}\" is not found");
                }

                try
                {
                    using (var file = File.OpenWrite(path))
                    {
                        resource.CopyTo(file);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}