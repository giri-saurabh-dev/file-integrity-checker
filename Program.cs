using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string fileListPath = "filelist.txt";
        if (!File.Exists(fileListPath))
        {
            Console.WriteLine("[ERROR] filelist.txt not found.");
            return;
        }

        var filesToMonitor = File.ReadAllLines(fileListPath);
        var hashStore = new HashStore();

        foreach (var filePath in filesToMonitor)
        {
            if (!File.Exists(filePath))
            {
                AlertService.ShowAlert($"File not found: {filePath}");
                continue;
            }

            string currentHash = FileMonitor.ComputeFileHash(filePath);
            string storedHash = hashStore.GetHash(filePath);

            if (storedHash == null)
            {
                // First time: store hash
                hashStore.SetHash(filePath, currentHash);
                AlertService.ShowInfo($"Initial hash stored for: {filePath}");
            }
            else if (FileMonitor.HasFileChanged(filePath, storedHash))
            {
                AlertService.ShowAlert($"⚠️ File modified: {filePath}");
            }
            else
            {
                AlertService.ShowInfo($"File OK: {filePath}");
            }
        }

        hashStore.Save();
    }
}
