using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableItem : InteractableItem
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _resetItemYPos = -10;

    private RaycastHit[] _hits;
    private Rigidbody _rigidbody;
    private PlayerInteractor _playerInteractor;
    private Vector3 _targetPosition;
    private Vector3 _previousPosition;
    private Quaternion _previousRotation;
    private bool _isPickedUp;

    public override bool TryInteract()
    {
        if (_isPickedUp == false)
            PickUp();
        else
            PickDown();

        return true;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInteractor = FindObjectOfType<PlayerInteractor>();
    }

    private void Start()
    {
        _targetPosition = _playerInteractor.HandlePoint.transform.position;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        _hits = Physics.RaycastAll(_playerInteractor.HandlePoint.position,
        _playerInteractor.HandlePoint.transform.forward,
        _playerInteractor.MinDistance,
        _layerMask.value);

        Move();
    }

    private void FixedUpdate()
    {
        if (_isPickedUp)
        {
            _rigidbody.rotation = _previousRotation;

            if (_hits.Length > 0)
                _rigidbody.MovePosition(_hits[0].point);
            else
                _rigidbody.MovePosition(_targetPosition);
        }

        if (transform.position.y < _resetItemYPos)
            ResetPosition();
    }

    private void Move()
    {
        if (_hits.Length > 0)
            _targetPosition = _hits[0].point;
        else
            _targetPosition = _playerInteractor.HandlePoint.position + _playerInteractor.HandlePoint.forward * _playerInteractor.MinDistance;
    }

    private void PickUp()
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
        _isPickedUp = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void PickDown()
    {
        _isPickedUp = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    private void ResetPosition()
    {
        PickDown();
        transform.position = _previousPosition;
    }
}
