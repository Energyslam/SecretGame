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

    private List<IObservable> observables = new List<IObservable>();

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
        NotifyAllObservables(GetCurrentState());
        currentState = State.TRANSFORMING;

        if (newState == State.OVERWORLD)
        {
            underworldObjects.SetActive(false);
            overworldObjects.SetActive(true);
        }
        else if (newState == State.UNDERWORLD)
        {
            overworldObjects.SetActive(false);
            underworldObjects.SetActive(true);
        }

        // Spawn transform effect

        StartCoroutine(TransformWorld(newState));
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public float GetTransformTime()
    {
        return transformTime;
    }

    private IEnumerator TransformWorld(State newState)
    {
        yield return new WaitForSeconds(transformTime);
        currentState = newState;
    }

    public void AddObservable(IObservable go)
    {
        observables.Add(go);
    }

    public void RemoveObservable(IObservable go)
    {
        try
        {
            observables.Remove(go);
        }
        catch
        {

        }
    }

    public void NotifyAllObservables(WorldState.State state)
    {
        for (int i = 0; i < observables.Count; i++)
        {
            observables[i].Notify(state);
        }
    }
}
