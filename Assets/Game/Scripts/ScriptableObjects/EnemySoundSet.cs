using UnityEngine;

[CreateAssetMenu(menuName = "Audio/EnemySoundSet")]
public class EnemySoundSet : ScriptableObject
{
    public AudioClip hitSound;
    public AudioClip deathSound;
}
