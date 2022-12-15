using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 2f;
    
    private Transform _handlePoint;

    private bool _isInteractAvailable = true;

    public event UnityAction <InputValue> Scrolled;

    public Transform HandlePoint => _handlePoint.transform;
    public float MaxDistance => _maxDistance;

    public void OnInteract(InputValue value)
    {
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _maxDistance);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.TryGetComponent<InteractableItem>(out InteractableItem item))
                _isInteractAvailable = !item.TryInteract();
        }
    }

    public void OnScroll(InputValue value)
    {
        Scrolled?.Invoke(value);
    }

    private void Start()
    {
        _handlePoint = _camera.transform;
    }

    private void OnValidate()
    {
        if (_camera == null)
            throw new ArgumentNullException("Camera", "Cannot be null");
    }
}
