using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupItemUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private int _money;
    [SerializeField] private List<AudioClip> _audioClip;
    [SerializeField] private Vector2 _pitchDiaposon;
    public Canvas Canvas => _canvas;
    public TextMeshProUGUI Text => _text;
    public int Money => _money;
    public GameObject Mesh => _mesh;

    public void OnPickup()
    {
        AudioClip clip = _audioClip[Random.Range(0, _audioClip.Count)];
        EventBus.Publish(new PitchedSoundEvent(this.gameObject, clip, _pitchDiaposon));
    }
}
