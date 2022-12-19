using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(MeshCollider))]
public class HiddenObject : MonoBehaviour
{
    [SerializeField] private LeaveTrigger _leaveTrigger;

    private MeshCollider _meshCollider;
    private Material _material;
    private Color _previousColor;
    private float _minAlpha = 0;
    private float _maxAlpha = 1;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _material = GetComponent<MeshRenderer>().material;
        _previousColor = _material.color;
    }

    private void Start()
    {
        _material.color = new Color(_previousColor.r, _previousColor.g, _previousColor.b, _minAlpha);
        _meshCollider.enabled = false;
    }
    private void OnEnable()
    {
        _leaveTrigger.Leaved += MakeVisible;
    }

    private void OnDisable()
    {
        _leaveTrigger.Leaved -= MakeVisible;
    }

    private void MakeVisible(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractor>(out PlayerInteractor playerInteractor))
        {
            _meshCollider.enabled = true;
            _material.color = new UnityEngine.Color(_previousColor.r, _previousColor.g, _previousColor.b, _maxAlpha);
        }
    }
}
