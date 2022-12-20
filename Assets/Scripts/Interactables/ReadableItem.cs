using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReadableItem : InteractableItem
{
    [SerializeField][TextArea] private string[] _texts;
    [SerializeField] private Trigger _trigger;
    
    private TextPanel _textPanel;

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
        _textPanel = GameObject.FindObjectOfType<TextPanel>();
        _trigger = GetComponentInChildren<Trigger>();
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
