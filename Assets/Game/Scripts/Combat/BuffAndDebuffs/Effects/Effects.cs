using UnityEngine;


public abstract class Effects : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private float _duration;
    [SerializeField] private bool _canStack = false;
    private float _timeLeft = 0;
    public float Duration => _duration;
    public bool IsActive => _timeLeft > 0;
    public GameObject Owner { get; private set; }
    public string Id => string.IsNullOrEmpty(_id) ? name : _id;
    public bool CanStack => _canStack;
    public void Initialize(GameObject owner)
    {
        Owner = owner;
        _timeLeft = _duration;
        OnApply();
    }

    public virtual void Tick(float deltaTime)
    {
        if (!IsActive) return;
        
        _timeLeft -= deltaTime;
        OnUpdate(deltaTime);
        
        if (_timeLeft <= 0f)
        {
            OnEnd();
        }
    }

    public void ResetDuration()
    {
        _timeLeft = _duration;
    }
    
    protected abstract void OnApply();
    protected abstract void OnUpdate(float deltaTime);
    protected abstract void OnEnd();
}
