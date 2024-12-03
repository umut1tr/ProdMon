using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ProdMon.Application.Interfaces;
using ProdMon.Domain.Models;
using Newtonsoft.Json;
using Application.Interfaces;

namespace ProdMon.Application.Services
{
    public class FileWatcherService : IFileWatcherService, IHostedService
    {
        private readonly string _filePath;
        private readonly string _lastCheckedFilePath;
        private readonly ILogger<FileWatcherService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private FileSystemWatcher _watcher;

        public FileWatcherService(string filePath, string lastCheckedFilePath, ILogger<FileWatcherService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _filePath = filePath;
            _lastCheckedFilePath = lastCheckedFilePath;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Ensure the directory exists
            if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
            {
                throw new ArgumentException($"The directory name '{Path.GetDirectoryName(_filePath)}' does not exist.");
            }

            _watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(_filePath),
                Filter = Path.GetFileName(_filePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };

            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;

            _logger.LogInformation($"Started watching file: {_filePath}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher?.Dispose();
            _logger.LogInformation($"Stopped watching file: {_filePath}");
            return Task.CompletedTask;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {

            // Adding a short delay after scan happened and file got appended.
            Task.Delay(500).Wait();

            if (IsFileLocked(new FileInfo(_filePath)))
            {
                _logger.LogInformation($"File {_filePath} is currently in use by another process.");
                Task.Delay(500).Wait();
                return;
            }
                        

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var entryRepository = scope.ServiceProvider.GetRequiredService<IEntryRepository>();

                try
                {
                    DateTime lastCheckedTime = LoadLastCheckedTime();
                    string jsonContent = File.ReadAllText(_filePath);
                    List<MonitorEntry> entries = JsonConvert.DeserializeObject<List<MonitorEntry>>(jsonContent);

                    var newEntries = new List<MonitorEntry>();

                    foreach (var entry in entries)
                    {
                        if (entry.Timestamp == default(DateTime))
                        {
                            _logger.LogError($"Invalid Timestamp for entry with Dmc: {entry.Dmc}");
                            continue;
                        }

                        if (entry.Timestamp > lastCheckedTime)
                        {
                            _logger.LogInformation($"Timestamp: {entry.Timestamp}");
                            _logger.LogInformation($"Dmc: {entry.Dmc}");
                            _logger.LogInformation($"Description: {entry.Description}");
                            _logger.LogInformation($"Quality: {entry.Quality}");

                            newEntries.Add(entry);
                            lastCheckedTime = entry.Timestamp;
                            SaveLastCheckedTime(lastCheckedTime);
                        }
                    }

                    foreach (var entry in newEntries)
                    {
                        entryRepository.AddEntryAsync(entry).Wait();
                        _logger.LogInformation("New entry has been saved to the database.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing file: {ex.Message}");
                }
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        private void SaveLastCheckedTime(DateTime lastCheckedTime)
        {
            File.WriteAllText(_lastCheckedFilePath, lastCheckedTime.ToString("dd.MM.yyyy HH:mm:ss"));
        }

        private DateTime LoadLastCheckedTime()
        {
            if (File.Exists(_lastCheckedFilePath))
            {
                try
                {
                    string lastCheckedTimeStr = File.ReadAllText(_lastCheckedFilePath);
                    return DateTime.Parse(lastCheckedTimeStr);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return DateTime.MinValue;
        }
    }
}
