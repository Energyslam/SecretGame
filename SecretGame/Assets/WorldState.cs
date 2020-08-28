using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour, IObserver
{
    #region Singleton
    public static WorldState _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    #endregion

    public enum State { OVERWORLD, UNDERWORLD, TRANSFORMING }

    [SerializeField]
    private State currentState = State.OVERWORLD;

    [SerializeField]
    private float transformTime = 1.0f;

    [SerializeField]
    private GameObject overworldObjects, underworldObjects;

    private List<IObservable> observables;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GetCurrentState() == State.OVERWORLD)
            {
                ChangeCurrentState(State.UNDERWORLD);
            }
            else if (GetCurrentState() == State.UNDERWORLD)
            {
                ChangeCurrentState(State.OVERWORLD);
            }
        }
    }

    public void ChangeCurrentState(State newState)
    {
        currentState = State.TRANSFORMING;

        if (newState == State.OVERWORLD)
        {
            underworldObjects.SetActive(false);
        }
        else if (newState == State.UNDERWORLD)
        {
            overworldObjects.SetActive(false);
        }

        // Spawn transform effect

        StartCoroutine(TransformWorld(newState));
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private IEnumerator TransformWorld(State newState)
    {
        yield return new WaitForSeconds(transformTime);
        currentState = newState;
    }

    public void AddObservable(IObservable obs)
    {
        observables.Add(obs);
    }

    public void RemoveObservable(IObservable obs)
    {
        try
        {
            observables.Remove(obs);
        }
        catch
        {

        }
    }

    public void NotifyAllObservables()
    {
        for (int i = 0; i < observables.Count; i++)
        {
            observables[i].Notify();
        }
    }
}
