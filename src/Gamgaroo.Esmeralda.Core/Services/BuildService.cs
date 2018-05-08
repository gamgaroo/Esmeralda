using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Gamgaroo.Esmeralda.Core.Models;
using Gamgaroo.Esmeralda.Core.Options;

namespace Gamgaroo.Esmeralda.Core.Services
{
    public sealed class BuildService : IBuildService
    {
        private readonly IBuildStatusService _buildStatusService;
        private readonly IDownloadService _downloadService;
        private readonly BuildServiceOptions _options;

        public BuildService(
            BuildServiceOptions options,
            IBuildStatusService buildStatusService,
            IDownloadService downloadService)
        {
            _options = options;
            _buildStatusService = buildStatusService;
            _downloadService = downloadService;
        }

        public async Task HandleAsync(WebHookModel model, string wwwroot)
        {
            var buildStatusUrl = $"{_options.CloudBaseUrl}{model.Links.Api_Self.Href}";
            var buildStatusModel = await _buildStatusService.GetBuildStatusModelAsync(buildStatusUrl, _options.ApiKey);

            var downloadUrl = $"{buildStatusModel.Links.Download_Primary.Href}";
            using (var stream = await _downloadService.DownloadAsStreamAsync(downloadUrl))
            {
                var zipDirectory = GetZipDirectory(wwwroot);

                if (!Directory.Exists(zipDirectory))
                    Directory.CreateDirectory(zipDirectory);

                var path = $"{zipDirectory}\\{buildStatusModel.Build}.zip";

                SaveZipFile(stream, path);
                UnzipFileToWwwRoot(path, wwwroot);
            }
        }

        private static string GetTempDirectory(string wwwroot)
        {
            return $"{wwwroot}\\Temp";
        }

        private static string GetZipDirectory(string wwwroot)
        {
            return $"{wwwroot}\\Zip";
        }

        private static void SaveZipFile(Stream stream, string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            var zipFileStream = File.Create(path);
            stream.CopyTo(zipFileStream);

            zipFileStream.Close();
        }

        private static void UnzipFileToWwwRoot(string zipFile, string wwwroot)
        {
            var tempDirectory = GetTempDirectory(wwwroot);

            if (Directory.Exists(tempDirectory))
                Directory.Delete(tempDirectory, true);

            Directory.CreateDirectory(tempDirectory);

            // Extract Zip
            using (var zipFileStream = File.OpenRead(zipFile))
            using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(tempDirectory);
            }

            // Move all files and directories
            var buildDirectory = Directory.GetDirectories(tempDirectory).Single();

            foreach (var directory in Directory.GetDirectories(buildDirectory))
            {
                var dist = $"{wwwroot}\\{new DirectoryInfo(directory).Name}";

                if (Directory.Exists(dist))
                    Directory.Delete(dist, true);

                Directory.Move(directory, dist);
            }

            foreach (var file in Directory.GetFiles(buildDirectory))
            {
                var dist = $"{wwwroot}\\{new FileInfo(file).Name}";

                if (File.Exists(dist))
                    File.Delete(dist);

                File.Move(file, dist);
            }

            Directory.Delete(tempDirectory, true);
        }
    }
}