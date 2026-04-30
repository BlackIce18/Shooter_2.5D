using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static Camera MainCamera;
    
    public void Initialize()
    {
        SetGameSpeed(1.5f);
        if (MainCamera == null) GameSettings.MainCamera = Camera.main;
    }

    private void SetGameSpeed(float speed)
    {
        if(speed < 0) return;
        
        Time.timeScale = speed;
    }
}
