using TMPro;
using UnityEngine;

public class AreaInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    public TextMeshProUGUI Title => _title;
    public TextMeshProUGUI Description => _description;
}
