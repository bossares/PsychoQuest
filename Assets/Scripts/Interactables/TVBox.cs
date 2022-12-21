using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TVBox : SwitchableItem
{
    [SerializeField] private TVImage _image;

    private AudioSource _audioSource;

    protected override void ToggleIsActive()
    {
        base.ToggleIsActive();

        _image.SetActive(IsActive);

        if (IsActive)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _image.SetActive(IsActive);

        if (IsActive)
            _audioSource.Play();
    }
}
