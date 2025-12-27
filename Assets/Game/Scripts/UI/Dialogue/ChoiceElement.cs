using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private GameObject _container;

    public Image Image => _image;
    public TextMeshProUGUI Name => _name;
    public GameObject Container => _container;
}
