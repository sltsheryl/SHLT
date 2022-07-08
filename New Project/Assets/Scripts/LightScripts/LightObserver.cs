using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObserver : Observer<bool>
{
    private EachLight light;
    private LightSubject lightSubject;

    public LightObserver(EachLight light, LightSubject lightSubject)
    {
        this.light = light;
        this.lightSubject = lightSubject;
        this.lightSubject.Subscribe(this);
    }

    public override void OnNotify(bool notif)
    {
        light.UpdateState(notif);
    }
}
