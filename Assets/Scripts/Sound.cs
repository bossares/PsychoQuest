using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [SerializeField] private float _delayInSeconds = 0f;

    private AudioSource _audioSource;
    private Coroutine _previousCoroutine = null;

    public void Play()
    {
        if (_previousCoroutine != null)
            StopCoroutine(_previousCoroutine);

        _previousCoroutine = StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_delayInSeconds);

        yield return delay;

        _audioSource.Play();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
