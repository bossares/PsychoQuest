using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class TVBox : SwitchableItem
{
    [SerializeField] private GameObject _image;

    private AudioSource _audioSource;

    public override void ToggleIsActive()
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
