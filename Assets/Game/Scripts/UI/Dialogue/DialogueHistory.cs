using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHistory : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueElementPrefab;
    [SerializeField] private GameObject _dialogueElementRightPrefab;
    public GameObject dialogueElementParent;
    public ScrollRect scrollRect;
    public void AddToHistory(string message, bool leftSideImage = true)
    {
        GameObject instance = !leftSideImage ? Instantiate(_dialogueElementRightPrefab, dialogueElementParent.transform) : Instantiate(_dialogueElementPrefab, dialogueElementParent.transform);
        DialogueElement historyElement = instance.GetComponent<DialogueElement>();
        //instance.transform.SetAsFirstSibling();
        //historyElement.name = dialogueElement.name;
        Debug.Log(message);
        historyElement.Message.text = message;
        //historyElement.Image.sprite = dialogueElement.Image.sprite;
        
        StartCoroutine(ScrollDown());
    }

    public IEnumerator ScrollDown()
    {
        yield return null;
        yield return new WaitForEndOfFrame();
        
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
