using UnityEngine;

// Заполнение характеристик с помощью ScriptableObject
[CreateAssetMenu(menuName = "Data/InfoData")]
public class InfoData : ScriptableObject
{
    public string title;
    public string description;
}