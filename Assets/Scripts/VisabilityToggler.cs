using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(MeshCollider))]
public class VisabilityToggler : MonoBehaviour
{
    [SerializeField] private TriggerBase _trigger;
    [SerializeField] private bool _isActiveColliderStart = false;
    [SerializeField] private bool _isActiveColliderEnd = true;
    [SerializeField] private float _startAlpha = 0f;
    [SerializeField] private float _endAlpha = 1.0f;

    private MeshCollider _meshCollider;
    private Material _material;
    private Color _previousColor;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _material = GetComponent<MeshRenderer>().material;
        _previousColor = _material.color;
    }

    private void Start()
    {
        _material.color = new Color(_previousColor.r, _previousColor.g, _previousColor.b, _startAlpha);
        _meshCollider.enabled = _isActiveColliderStart;
    }
    private void OnEnable()
    {
        _trigger.Involved += ToggleVisability;
    }

    private void OnDisable()
    {
        _trigger.Involved -= ToggleVisability;
    }

    private void ToggleVisability(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractor>(out PlayerInteractor playerInteractor))
        {
            _meshCollider.enabled = _isActiveColliderEnd;
            _material.color = new UnityEngine.Color(_previousColor.r, _previousColor.g, _previousColor.b, _endAlpha);
        }
    }
}
