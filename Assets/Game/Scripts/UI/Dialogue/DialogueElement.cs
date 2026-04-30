using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _message;

    public Image Image => _image;
    public TextMeshProUGUI Name => _name;
    public TextMeshProUGUI Message => _message;
}
