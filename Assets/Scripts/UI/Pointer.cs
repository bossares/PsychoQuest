using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Pointer : MonoBehaviour
{
    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private float _targetAlpha = 0.75f;

    private readonly float _minAlpha = 0;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.color = new Color(_image.color.r, _image.color.b, _image.color.b, 0);
    }

    private void OnEnable()
    {
        _playerInteractor.IsInteractAvailableToggled += OnToggleVisability;
    }

    private void OnDisable()
    {
        _playerInteractor.IsInteractAvailableToggled -= OnToggleVisability;
    }

    private void OnToggleVisability(bool visible)
    {
        Color previousColor = _image.color;

        _image.color = new Color(previousColor.r, previousColor.g, previousColor.b, visible ? _targetAlpha : _minAlpha);
    }
}
