using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _minDistance = 2f;
    
    private Transform _handlePoint;

    private bool _isInteractAvailable = true;

    public Transform HandlePoint => _handlePoint.transform;
    public float MinDistance => _minDistance;

    public void OnInteract(InputValue value)
    {
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _minDistance);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.TryGetComponent<InteractableItem>(out InteractableItem item))
                _isInteractAvailable = !item.TryInteract();
        }
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
