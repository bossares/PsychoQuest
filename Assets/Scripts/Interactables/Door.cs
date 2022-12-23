using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Door : SwitchableItem
{
    [SerializeField] private AudioClip _lockedSound;
    [SerializeField] private AudioClip _openSound;

    private readonly float _openedDegrees = 90f;
    private readonly float _closedDegrees = 0;
    private AudioSource _audioSource;
    private HingeJoint _hingeJoint;
    private Rigidbody _rigidbody;
    private JointSpring _jointSpring;
    private Coroutine _previousCoroutine = null;

    public override bool TryInteract()
    {
        PlaySound();

        return base.TryInteract();
    }

    public override void ToggleIsActive()
    {
        base.ToggleIsActive();

        if (_previousCoroutine != null)
            StopCoroutine(_previousCoroutine);

        _previousCoroutine = StartCoroutine(ToggleCoroutine());
    }

    private IEnumerator ToggleCoroutine()
    {
        float stepInSeconds = 0.1f;
        WaitForSeconds delay = new WaitForSeconds(stepInSeconds);

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

    private void PlaySound()
    {
        if (IsBlocked)
            _audioSource.PlayOneShot(_lockedSound);
        else
            _audioSource.PlayOneShot(_openSound);
    }

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
        _jointSpring = _hingeJoint.spring;

        if (IsActive)
            _previousCoroutine = StartCoroutine(ToggleCoroutine());
    }
}
