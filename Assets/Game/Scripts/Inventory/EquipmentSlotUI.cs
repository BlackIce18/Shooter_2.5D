using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Image _image;

    public Image Image => _image;
}
