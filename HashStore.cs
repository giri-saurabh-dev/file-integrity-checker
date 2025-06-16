using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class HashStore
{
    private const string HashStoreFile = "hash_store.json";
    private Dictionary<string, string> hashDict;

    public HashStore()
    {
        if (File.Exists(HashStoreFile))
        {
            string json = File.ReadAllText(HashStoreFile);
            hashDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();
        }
        else
        {
            hashDict = new Dictionary<string, string>();
        }
    }

    public string GetHash(string filePath)
    {
        return hashDict.ContainsKey(filePath) ? hashDict[filePath] : null;
    }

    public void SetHash(string filePath, string hash)
    {
        hashDict[filePath] = hash;
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(hashDict, Formatting.Indented);
        File.WriteAllText(HashStoreFile, json);
    }

    public Dictionary<string, string> GetAllHashes()
    {
        return new Dictionary<string, string>(hashDict);
    }
}
