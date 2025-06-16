using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class FileMonitor
{
    // Generate SHA256 hash of a file
    public static string ComputeFileHash(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }

    // Compare current file hash to stored hash
    public static bool HasFileChanged(string filePath, string storedHash)
    {
        try
        {
            string currentHash = ComputeFileHash(filePath);
            return currentHash != storedHash;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Could not hash {filePath}: {ex.Message}");
            return false;
        }
    }
}
