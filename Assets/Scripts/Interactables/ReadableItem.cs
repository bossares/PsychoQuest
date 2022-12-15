using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ReadableItem : InteractableItem
{
    [SerializeField][TextArea] private string[] _text;
    
    private TextPanel _textPanel;
    private LeaveTrigger _leaveTrigger;

    public override bool TryInteract()
    {
        _textPanel.ShowParagraphs(_text);

        return true;
    }

    private void Awake()
    {
        _textPanel = GameObject.FindObjectOfType<TextPanel>();
        _leaveTrigger = GetComponentInChildren<LeaveTrigger>();
    }

    private void OnEnable()
    {
        _leaveTrigger.Leaved += OnLeave;
    }

    private void OnDisable()
    {
        _leaveTrigger.Leaved += OnLeave;
    }

    private void OnLeave(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInteractor>(out PlayerInteractor _playerInteractor))
            _textPanel.Hide();
    }

    private void OnValidate()
    {
        if (_text.Length == 0 || _text.Any(text => text.Length == 0))
            throw new ArgumentException("Cannot be empty", "Text");
    }
}
