using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    [SerializeField] private TextAsset localizationFile;
    private Dictionary<string, string> _dict = new();

    private void Awake()
    {
        Instance = this;
        Load();
    }

    private void Load()
    {
        _dict.Clear();
        
        // key=value
        var lines = localizationFile.text.Split('\n');
        foreach (var line in lines)
        {
            if(string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

            var split = line.Split('=');
            if(split.Length < 2) continue;

            _dict[split[0].Trim()] = split[1].Trim();
        }
    }

    public string Get(string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;

        return _dict.TryGetValue(key, out var value) ? value : $"#{key}";
    }
}
