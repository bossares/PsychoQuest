using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Door : SwitchableItem
{
    private HingeJoint _hingeJoint;
    private Rigidbody _rigidbody;
    private JointSpring _jointSpring;
    private float _openedDegrees = 90f;
    private float _closedDegrees = 0;
    private Coroutine _previousCoroutine = null;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
        _jointSpring = _hingeJoint.spring;

        if (IsActive)
            _previousCoroutine = StartCoroutine(ToggleCoroutine());
    }

    protected override void ToggleIsActive()
    {
        base.ToggleIsActive();

        if (_previousCoroutine != null)
            StopCoroutine(_previousCoroutine);

        _previousCoroutine = StartCoroutine(ToggleCoroutine());
    }

    private IEnumerator ToggleCoroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        _rigidbody.isKinematic = false;
        ToggleTargetDegrees();

        yield return delay;

        while (_rigidbody.velocity.normalized != Vector3.zero)
            yield return delay;

        _rigidbody.isKinematic = true;
    }

    private void ToggleTargetDegrees()
    {
        _jointSpring = _hingeJoint.spring;
        _jointSpring.targetPosition = IsActive ? _openedDegrees : _closedDegrees;

        _hingeJoint.spring = _jointSpring;
    }
}
