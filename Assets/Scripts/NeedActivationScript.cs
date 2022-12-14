using UnityEngine;
using UnityEngine.Events;

public class NeedActivationScript : MonoBehaviour
{
    public UnityEvent OnActivate;

    private bool _activated;

    public bool Activated { get { return _activated; } }

    public void Activate()
    {
        OnActivate?.Invoke();
        _activated = true;
    }   
}
