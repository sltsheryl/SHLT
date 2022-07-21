using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverSubject<T> : MonoBehaviour
{
    private List<Observer<T>> observers = new List<Observer<T>>();

    public void Subscribe(Observer<T> observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(Observer<T> observer)
    {
        observers.Remove(observer);
    }

    public void DispatchNotifications(T notif)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify(notif);
        }
    }
}
