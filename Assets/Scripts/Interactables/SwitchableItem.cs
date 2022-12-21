using UnityEngine;
using UnityEngine.Events;

public class SwitchableItem : InteractableItem
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private bool _isBlocked = false;

    public event UnityAction <bool> IsBlockedToggled;
    public event UnityAction <bool> IsActiveToggled;

    public bool IsActive => _isActive;
    public bool IsBlocked => _isBlocked;

    public override bool TryInteract()
    {
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
        IsBlockedToggled?.Invoke(_isBlocked);
    }

    protected virtual void ToggleIsActive()
    {
        _isActive = !_isActive;
        IsActiveToggled?.Invoke(_isActive);
    }
}
