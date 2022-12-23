using UnityEngine;
using UnityEngine.Events;

public class SwitchableItem : InteractableItem
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private bool _isBlocked = false;
    [SerializeField] private UnityEvent _onIsFistTimeActivated;

    private bool _isFirstTimeActivated = false;

    public bool IsActive => _isActive;
    public bool IsBlocked => _isBlocked;

    public override bool TryInteract()
    {
        if (_isFirstTimeActivated == false)
        {
            _isFirstTimeActivated = true;
            _onIsFistTimeActivated?.Invoke();
        }

        if (_isBlocked == false)
        {
            ToggleIsActive();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ToggleIsBlocked()
    {
        _isBlocked = !_isBlocked;
    }

    public virtual void ToggleIsActive()
    {
        _isActive = !_isActive;
    }
}
