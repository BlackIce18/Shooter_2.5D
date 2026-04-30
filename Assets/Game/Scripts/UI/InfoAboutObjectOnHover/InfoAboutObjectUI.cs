using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoAboutObjectUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;

    public TextMeshProUGUI TextField => _text;
    public Slider Slider => _slider;
}

