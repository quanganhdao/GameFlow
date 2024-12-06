using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowListener : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFocused;
    [SerializeField] private UnityEvent _onCovered;
    [SerializeField] private UnityEvent _onActive;
    [SerializeField] private UnityEvent _onClosed;


    public void OnFocused()
    {
        _onFocused?.Invoke();
    }
    public void OnCovered()
    {
        _onCovered?.Invoke();
    }
    public void OnActive()
    {
        _onActive?.Invoke();
    }
    public void OnClosed()
    {
        _onClosed?.Invoke();
    }
}
