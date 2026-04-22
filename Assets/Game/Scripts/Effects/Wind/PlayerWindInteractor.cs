using UnityEngine;

public class PlayerWindInteractor : MonoBehaviour
{
    private static readonly int PlayerPosID = Shader.PropertyToID("_PlayerPos");

    void Update()
    {
        Vector3 pos = transform.position;
        Shader.SetGlobalVector(PlayerPosID, pos);
    }
}
