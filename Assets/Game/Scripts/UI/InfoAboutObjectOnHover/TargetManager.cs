using UnityEngine;
using UnityEngine.InputSystem;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private InfoAboutObjectUI _ui;
    private Camera _mainCamera;
    private InfoAboutObjectOnHover _currentHover;
    [SerializeField] private LayerMask hoverMask;

    public InfoAboutObjectOnHover CurrentTarget => _currentHover;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.value);
        
        InfoAboutObjectOnHover newHover = null;
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hoverMask))
        {
            hit.transform.TryGetComponent(out newHover);
            newHover.TryGetComponent(out HealthComponent healthComponent);
            
            _ui.Slider.maxValue = healthComponent.Max;
            _ui.Slider.value = healthComponent.Current;
        }

        if (_currentHover == newHover) return;
        
        if(_currentHover != null) Hide();

        _currentHover = newHover;
        
        if(_currentHover != null) Show(_currentHover.InfoData);
    }

    public void Show(InfoData data)
    {
        _ui.gameObject.SetActive(true);
        _ui.TextField.text = data.title;
    }

    public void Hide()
    {
        _ui.gameObject.SetActive(false);
        _ui.TextField.text = "";
    }
}
