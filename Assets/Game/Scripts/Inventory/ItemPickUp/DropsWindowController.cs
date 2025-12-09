using System;
using UnityEngine;

public class DropsWindowController : MonoBehaviour
{
    [SerializeField] private FloatingWindow _floatingWindow;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        var item = Instantiate(_prefab, new Vector3(-1.375f,0.129999995f,-8.5f), Quaternion.identity, transform).GetComponent<DroppedItem>();
        item.windowUI = _floatingWindow;

    }
    
    
}
