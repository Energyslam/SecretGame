using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void AddObservable(IObservable go);

    void RemoveObservable(IObservable go);

    void NotifyAllObservables(WorldState.State state);
}

public interface IObservable
{
    void SubscribeToObserver();

    void Notify(WorldState.State state);
}
