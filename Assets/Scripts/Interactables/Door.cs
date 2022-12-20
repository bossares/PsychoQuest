using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Door : InteractableItem
{
    [SerializeField] private bool _isBlocked = false;

    private HingeJoint _hingeJoint;
    private Rigidbody _rigidbody;
    private float _openedDegreses = 90f;
    private float _closedDegreses = 0;
    private Coroutine _previousCoroutine = null;

    public bool IsBlocked => _isBlocked;

    public override bool TryInteract()
    {
        if (_isBlocked == false)
        {
            ToggleOpened();
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

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
    }

    private void ToggleOpened()
    {
        if (_previousCoroutine != null)
            StopCoroutine( _previousCoroutine );

        _previousCoroutine = StartCoroutine(ToggleCoroutine());
    }

    private IEnumerator ToggleCoroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        JointSpring jointSpring = _hingeJoint.spring;
        _rigidbody.isKinematic = false;

        if (_hingeJoint.spring.targetPosition != _openedDegreses)
            jointSpring.targetPosition = _openedDegreses;
        else
            jointSpring.targetPosition = _closedDegreses;

        _hingeJoint.spring = jointSpring;
        yield return delay;

        while (_rigidbody.velocity.normalized != Vector3.zero)
            yield return delay;

        _rigidbody.isKinematic = true;
    }
}
