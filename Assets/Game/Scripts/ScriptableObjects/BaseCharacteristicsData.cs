using UnityEngine;

// Заполнение характеристик с помощью ScriptableObject
[CreateAssetMenu(menuName = "Data/Characteristics")]
public class BaseCharacteristicsData : ScriptableObject
{
    [SerializeField] private CharacteristicsData _characteristicsData;

    public CharacteristicsData CharacteristicsData => _characteristicsData;
}