using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightObserver : Observer<bool>
{
    [SerializeField] private LightSubject lightSubject;
    [SerializeField] private UnityEvent callback;

    private void Start()
    {
        lightSubject.Subscribe(this);
    }

    public override void OnNotify(bool notif)
    {
        callback.Invoke();
    }
}
