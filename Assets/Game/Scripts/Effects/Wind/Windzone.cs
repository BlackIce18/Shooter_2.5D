using UnityEngine;

public class Windzone : MonoBehaviour
{
    public float windStrength = 0.1f;
    public float windSpeed = 1f;

    void Update()
    {
        Shader.SetGlobalFloat("_WindStrength", windStrength);
        Shader.SetGlobalFloat("_WindSpeed", windSpeed);
    }
}
