using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoitaabScan.Model
{
    public class DirectoryMonitoring
    { 
        public delegate void ChangingDirectory();

        public event ChangingDirectory changesEvent;

        private string _path;
        public DirectoryMonitoring(string path)
        {
            _path = path;
            Start();
        }
        private void Start()
        {
            var watcher = new FileSystemWatcher(_path);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;

            
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        private  void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            File.AppendAllText("serverLogs.log", "changed" + e.FullPath + DateTime.Now + " \n");
            changesEvent.Invoke();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath} {DateTime.Now} \n";
            File.AppendAllText("serverLogs.log",value);

            changesEvent.Invoke();
        }

        private void OnDeleted(object sender, FileSystemEventArgs e) 
        { 
            var res = $"Deleted: {e.FullPath} {DateTime.Now} \n";
            File.AppendAllText("serverLogs.log", res);
            changesEvent.Invoke();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            var res = $"Renamed: Old: {e.OldFullPath}New: {e.FullPath} {DateTime.Now} \n";

            File.AppendAllText("serverLogs.log", res);
            changesEvent.Invoke();

        }
                    
    }
}
