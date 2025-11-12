using UnityEngine;

// Заполнение характеристик с помощью ScriptableObject
[CreateAssetMenu(menuName = "Data/Characteristics")]
public class BaseCharacteristicsData : ScriptableObject
{
    public CharacteristicsData characteristicsData;
}