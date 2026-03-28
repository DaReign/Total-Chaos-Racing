using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Base class for all UI Toolkit screen controllers.
/// Handles UIDocument access, root element caching, show/hide.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public abstract class UIScreenBase : MonoBehaviour
{
    protected UIDocument Document { get; private set; }
    protected VisualElement Root { get; private set; }
    bool _elementsReady;

    protected virtual void Awake()
    {
        Document = GetComponent<UIDocument>();
    }

    protected virtual void OnEnable()
    {
        Root = Document.rootVisualElement;
        TryInitElements();
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvents();
    }

    /// <summary>
    /// Call from LateUpdate to handle late UXML loading.
    /// Returns true when elements are ready.
    /// </summary>
    protected bool EnsureElements()
    {
        if (_elementsReady) return true;
        Root = Document.rootVisualElement;
        if (Root == null || Root.childCount == 0) return false;
        TryInitElements();
        return _elementsReady;
    }

    void TryInitElements()
    {
        if (_elementsReady) return;
        if (Root == null || Root.childCount == 0) return;
        QueryElements();
        _elementsReady = true;
    }

    /// <summary>Override to cache VisualElement references via Root.Q()</summary>
    protected abstract void QueryElements();

    /// <summary>Override to subscribe to RaceEvents</summary>
    protected abstract void SubscribeEvents();

    /// <summary>Override to unsubscribe from RaceEvents</summary>
    protected abstract void UnsubscribeEvents();

    public void Show() => Root.style.display = DisplayStyle.Flex;
    public void Hide() => Root.style.display = DisplayStyle.None;
}
