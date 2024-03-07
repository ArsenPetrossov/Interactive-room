using System;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private int _highlightIntensity = 4;    
    private Outline _outline;
    private Rigidbody _rigidbody;
    private bool _isKinematic;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }

    public void SwitchKinematic(bool value)
    {
        _rigidbody.isKinematic = value;
    }

    public void Throw(Vector3 direction,float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
    
}