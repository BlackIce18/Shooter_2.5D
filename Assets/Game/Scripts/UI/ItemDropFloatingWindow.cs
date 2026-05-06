using TMPro;
using UnityEngine;

public class ItemDropFloatingWindow : FloatingWindow
{
    [SerializeField] private TextMeshProUGUI _text;

    public void ChangeText(string text)
    {
        _text.text = text;
    }
}
