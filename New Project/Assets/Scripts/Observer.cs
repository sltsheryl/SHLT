using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer<T>
{
    public abstract void OnNotify(T notif);
}
