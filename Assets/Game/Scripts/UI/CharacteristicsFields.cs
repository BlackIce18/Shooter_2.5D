using TMPro;
using UnityEngine;

public class CharacteristicsFields : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _value;

    public TextMeshProUGUI Text => _text;
    public TextMeshProUGUI Value => _value;
}
