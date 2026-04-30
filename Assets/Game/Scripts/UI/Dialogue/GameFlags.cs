using System.Collections.Generic;
using UnityEngine;

public class GameFlags : MonoBehaviour
{
    private static HashSet<string> _flags = new();

    public static bool HasFlag(string id) => _flags.Contains(id);
    public static void SetFlag(string id) => _flags.Add(id);
}
