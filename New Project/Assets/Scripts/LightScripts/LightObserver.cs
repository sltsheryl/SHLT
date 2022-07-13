using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObserver : Observer<bool>
{
    private EachLight light;
    private Words words;
    private LightSubject lightSubject;
    private Words words1;
    private LightSubject lightSubject1;


    public LightObserver(EachLight light, LightSubject lightSubject)
    {
        this.light = light;
        this.lightSubject = lightSubject;
        this.lightSubject.Subscribe(this);
    }

    public LightObserver(Words words1, LightSubject lightSubject1)
    {
        this.words1 = words1;
        this.lightSubject1 = lightSubject1;
        this.lightSubject.Subscribe(this);
    }

    public override void OnNotify(bool notif)
    {
        light.UpdateState(notif);
    }
}
