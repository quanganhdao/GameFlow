using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName = "FlowMember", menuName = "Flow System/FlowMember")]
public abstract class FlowMember : ScriptableObject
{
    [SerializeField] protected AssetReference _reference;

    private FlowListener _flowListener;

    public AssetReference Reference { get => _reference; set => _reference = value; }
    public FlowListener FlowListener { get => _flowListener; set => _flowListener = value; }

    public void OnFocused()
    {
        _flowListener.OnFocused();
    }
    public void OnCovered()
    {
        _flowListener.OnCovered();
    }
    public void OnActive()
    {
        _flowListener.OnActive();
    }
    public void OnClosed()
    {
        _flowListener.OnClosed();
    }


}
