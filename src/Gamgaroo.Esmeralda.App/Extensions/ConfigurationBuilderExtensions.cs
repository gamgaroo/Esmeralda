using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Gamgaroo.Esmeralda.App.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        private static string SettingsDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Settings");

        public static IConfigurationBuilder AddSettings(this IConfigurationBuilder config)
        {
            foreach (var fileInfo in GetSettingFiles())
                config.AddJsonFile(Path.Combine(fileInfo.DirectoryName, fileInfo.Name), false, true);

            return config;
        }

        private static IEnumerable<FileInfo> GetSettingFiles()
        {
            return Directory
                .GetFiles(SettingsDirectory)
                .Where(f => f.EndsWith(".json"))
                .Select(f => new FileInfo(f));
        }
    }
}