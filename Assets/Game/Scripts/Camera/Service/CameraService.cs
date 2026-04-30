using UnityEngine;

public class CameraService : MonoBehaviour, ICameraService
{
    public CameraMovement cameraMovement;
    public void Initialize()
    {
        cameraMovement.Initialize();
    }
}
