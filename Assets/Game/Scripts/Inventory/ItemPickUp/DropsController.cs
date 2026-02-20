using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropsController : MonoBehaviour
{
    [SerializeField] private FloatingWindow _floatingWindow;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _prefab2;
    [SerializeField] private Transform _parent;
    private void Start()
    {
        //Spawn(_prefab, new Vector3(-1.375f, 0.129999995f, -8.5f), Quaternion.identity);
        Spawn(_prefab2, new Vector3(37.389f,0.869f,4f), new Quaternion(0, Random.Range(0, 360),0, 1));
    }

    public void Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var item = Instantiate(prefab, position, rotation, _parent).GetComponent<DroppedItem>();
        item.windowUI = _floatingWindow;
        item.textField = _text;
    }
}
