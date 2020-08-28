using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void AddObservable(IObservable obs);

    void RemoveObservable(IObservable obs);

    void NotifyAllObservables();
}

public interface IObservable
{
    void SubscribeToObserver();

    void Notify();
}
