using UnityEngine;
using UnityEngine.InputSystem;

public class InfoAboutObjectOnHover : MonoBehaviour
{
    [SerializeField] private InfoData _infoData;

    public InfoData InfoData => _infoData;
}
