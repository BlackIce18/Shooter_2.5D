using System;
using UnityEngine;

public class PlayNextAnimation : MonoBehaviour
{
    [SerializeField] private Animation _nextAnimation;
    private void PlayAnimation()
    {
        _nextAnimation.Play();
    }
}
