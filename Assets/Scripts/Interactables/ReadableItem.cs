using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReadableItem : InteractableItem
{
    [SerializeField][TextArea] private string[] _texts;
    [SerializeField] private TriggerBase _trigger;
    [SerializeField] private TextPanel _textPanel;

    public override bool TryInteract()
    {
        if (_textPanel.IsVisible)
            _textPanel.Hide();
        else
            _textPanel.ShowParagraphs(_texts);

        return true;
    }

    private void Awake()
    {
        _trigger = GetComponentInChildren<TriggerBase>();
    }

    private void OnEnable()
    {
        _trigger.Involved += OnLeave;
    }

    private void OnDisable()
    {
        _trigger.Involved += OnLeave;
    }

    private void OnLeave(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractor>(out PlayerInteractor _playerInteractor))
            _textPanel.Hide();
    }
}
