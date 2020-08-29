using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwapCameraMode : MonoBehaviour, IObservable
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Animator animator;

    public void Notify(WorldState.State state)
    {
        moveSpeed = WorldState._instance.GetTransformTime();

        if (state == WorldState.State.OVERWORLD)
        {
            MoveToShoulder();
        }
        else if (state == WorldState.State.UNDERWORLD)
        {
            MoveToTopDown();
        }
    }

    private void MoveToTopDown()
    {
        animator.SetFloat("Speed", moveSpeed); 
        animator.SetTrigger("PlayTopDown");
    }

    private void MoveToShoulder()
    {
        animator.SetFloat("Speed", moveSpeed);
        animator.SetTrigger("PlayShoulder");
    }

    public void SubscribeToObserver()
    {
        WorldState._instance.AddObservable(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SubscribeToObserver();

        moveSpeed = WorldState._instance.GetTransformTime();
    }
}
