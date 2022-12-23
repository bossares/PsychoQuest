using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Paragraph : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    public void Initialize(string text)
    {
        _textMeshPro.text = text;
    }

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }
}
