using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractZoneWithNpc : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private KeyBindings _keyBindings;
    [SerializeField] private KeyCommand _keyCommand;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _text.gameObject.SetActive(true);
            _keyBindings.BindInteract(_keyCommand);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _dialogueUI.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
            _keyBindings.BindInteract(null);
        }
    }
}
