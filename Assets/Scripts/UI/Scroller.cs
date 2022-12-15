using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 0.5f;

    private PlayerInteractor _playerInteractor;
    private Scrollbar _scrollbar;
    private float _minValue = 0;
    private float _maxValue = 1;

    private void Awake()
    {
        _playerInteractor = _player.GetComponent<PlayerInteractor>();
        _scrollbar = gameObject.GetComponentInChildren<Scrollbar>();
    }

    private void OnValidate()
    {
        if (_speed <= _minValue || _speed > _maxValue)
            throw new ArgumentOutOfRangeException("Speed", $"Must be greater than {_minValue} and less or equal {_maxValue}");
    }

    private void OnEnable()
    {
        _playerInteractor.Scrolled += OnScroll;
    }

    private void OnDisable()
    {
        _playerInteractor.Scrolled -= OnScroll;
    }

    private void OnScroll(InputValue value)
    {
        float targetValue = _scrollbar.value + _scrollbar.size * value.Get<Vector2>().normalized.y * _speed;

        _scrollbar.value = Mathf.Clamp(targetValue, _minValue, _maxValue);
    }
}
