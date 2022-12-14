using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TextPanel : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Paragraph _paragraphTemplate;
    [SerializeField] private int _maxParagraphsCapacity = 20;

    private ObjectPool<Paragraph> _paragraphsPool;
    private CanvasGroup _canvasGroup;
    private Coroutine _previousCoroutine = null;
    private int _paragraphsCount;

    public bool IsVisible => _canvasGroup.alpha > 0;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _paragraphsPool = new ObjectPool<Paragraph>(_paragraphTemplate, _container, _maxParagraphsCapacity);
    }

    public void ShowParagraphs(IReadOnlyList<string> texts)
    {
        if (_paragraphsCount > 0)
            ClearParagraphs();

        _previousCoroutine = StartCoroutine(ToggleVisability(true));
        _paragraphsCount = texts.Count;

        for (int i = 0; i < texts.Count; i++)
        {
            _paragraphsPool.TryGetObject(out Paragraph paragraph);
            paragraph.gameObject.SetActive(true);
            paragraph.Initialize(texts[i]);
        }
    }

    public void Hide()
    {
        if (_previousCoroutine != null)
            StopCoroutine(_previousCoroutine);

        _previousCoroutine = StartCoroutine(ToggleVisability(false));
    }

    private IEnumerator ToggleVisability(bool isShowing)
    {
        float stepInSeconds = 0.01f;
        WaitForSeconds _delay = new WaitForSeconds(stepInSeconds);
        float direction = isShowing ? 1 : -1;
        float minValue = 0;
        float maxValue = 1;
        float step = 0.05f;

        while (isShowing && _canvasGroup.alpha < maxValue ||
            isShowing == false && _canvasGroup.alpha > minValue)
        {
            _canvasGroup.alpha += step * direction;
            yield return _delay;
        }

        if (isShowing == false)
            ClearParagraphs();
    }

    private void ClearParagraphs()
    {
        _paragraphsPool.DeactivateAllItems();
        _paragraphsCount = 0;
    }
}
