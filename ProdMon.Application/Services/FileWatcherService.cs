using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProdMon.Domain.Models;

namespace Application.Services
{
    public class FileWatcherService : IHostedService
    {
        private readonly string _filePath;
        private readonly string _lastCheckedFilePath;
        private readonly ILogger<FileWatcherService> _logger;
        private FileSystemWatcher _watcher;

        public FileWatcherService(string filePath, string lastCheckedFilePath, ILogger<FileWatcherService> logger)
        {
            _filePath = filePath;
            _lastCheckedFilePath = lastCheckedFilePath;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
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
            if (IsFileLocked(new FileInfo(_filePath)))
            {
                _logger.LogInformation($"File {_filePath} is currently in use by another process.");
                return;
            }

            try
            {
                DateTime lastCheckedTime = LoadLastCheckedTime();
                string jsonContent = File.ReadAllText(_filePath);
                List<MonitorEntry> entries = JsonConvert.DeserializeObject<List<MonitorEntry>>(jsonContent);

                foreach (var entry in entries)
                {
                    if (entry.Timestamp > lastCheckedTime)
                    {
                        _logger.LogInformation($"Timestamp: {entry.Timestamp}");
                        _logger.LogInformation($"Dmc: {entry.Dmc}");
                        _logger.LogInformation($"Description: {entry.Description}");
                        _logger.LogInformation($"Quality: {entry.Quality}");

                        string articleNumber = ExtractArticleNumber(entry.Dmc);
                        _logger.LogInformation($"Extracted Article Number: {articleNumber}\n");

                        lastCheckedTime = entry.Timestamp;
                        SaveLastCheckedTime(lastCheckedTime);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing file: {ex.Message}");
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

        private string ExtractArticleNumber(long dmc)
        {
            string articleNumber = "55322234";
            string dmcStr = dmc.ToString();
            if (dmcStr.Contains(articleNumber))
            {
                return articleNumber;
            }
            return "Article Number not found";
        }

        private void SaveLastCheckedTime(DateTime lastCheckedTime)
        {
            File.WriteAllText(_lastCheckedFilePath, lastCheckedTime.ToString("o"));
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
                    throw (e);
                }
            }
            return DateTime.MinValue;
        }
    }
}
