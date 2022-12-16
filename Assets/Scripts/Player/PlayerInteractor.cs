using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 2f;
    
    private Transform _handlePoint;

    private bool _isInteractAvailable = false;
    private Coroutine _currentCoroutine;

    public event UnityAction <InputValue> Scrolled;
    public event UnityAction<bool> IsInteractAvailableToggled;

    public Transform HandlePoint => _handlePoint.transform;
    public float MaxDistance => _maxDistance;

    public void OnInteract(InputValue value)
    {
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _maxDistance);

        if (_isInteractAvailable)
        {
            hitInfo.collider.gameObject.TryGetComponent<InteractableItem>(out InteractableItem item);

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
        _currentCoroutine = StartCoroutine(IsInteractAvailableCoroutine());
    }

    private void OnValidate()
    {
        if (_camera == null)
            throw new ArgumentNullException("Camera", "Cannot be null");
    }

    private void OnDisable()
    {
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator IsInteractAvailableCoroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);

        while (true)
        {
            bool previousValue = _isInteractAvailable;

            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _maxDistance);

            _isInteractAvailable = hitInfo.collider != null &&
                hitInfo.collider.gameObject.TryGetComponent<InteractableItem>(out InteractableItem item);

            if (_isInteractAvailable != previousValue)
                IsInteractAvailableToggled?.Invoke(_isInteractAvailable);

            yield return delay;
        }
    }
}
