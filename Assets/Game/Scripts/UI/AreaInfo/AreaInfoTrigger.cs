using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AreaInfoUI))]
public class AreaInfoTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _leftInfoCanvas;
    [SerializeField] private AreaInfoScriptableObject _areaInfoData;
    [SerializeField] private AreaInfoUI _areaInfoUI;

    private void Start()
    {
        _areaInfoUI = GetComponent<AreaInfoUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!_leftInfoCanvas.activeSelf)
                StartCoroutine(TimeToHide());
        }
    }

    private IEnumerator TimeToHide()
    {
        ShowText();
        yield return new WaitForSeconds(5f);
        HideText();
    }

    private void ShowText()
    {
        _leftInfoCanvas.gameObject.SetActive(true);
        _areaInfoUI.Title.text = _areaInfoData.title;
        _areaInfoUI.Description.text = _areaInfoData.description;
    }

    private void HideText()
    {
        _leftInfoCanvas.gameObject.SetActive(false);
        _areaInfoUI.Title.text = "";
        _areaInfoUI.Description.text = "";
    }
}
