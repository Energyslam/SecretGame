using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeedChange = 0.15f;
    public float strafeSpeedChane =0.15f;

    public float maxMovementSpeed = 4f;
    public float maxStrafeSpeed = 3f;

    public float currentMovementSpeed;
    public float currentStrafeSpeed;

    public float currentMovementSpeedAnim;
    public float currentStrafeSpeedAnim;
    Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (currentMovementSpeed < 0)
            {
                currentMovementSpeed = 0;
                currentMovementSpeedAnim += movementSpeedChange * 3f;
            }
            currentMovementSpeed += movementSpeedChange;
            currentMovementSpeedAnim += movementSpeedChange;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentMovementSpeed += movementSpeedChange;
                currentMovementSpeed += movementSpeedChange;
                currentMovementSpeedAnim = Mathf.Clamp(currentMovementSpeedAnim, -1f, 2f);
                currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, -1f, 2f);
            }
            else
            {
                currentMovementSpeedAnim = Mathf.Clamp(currentMovementSpeedAnim, -1f, 1f);
                currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, -1f, 1f);
            }

            this.transform.Translate(this.transform.forward * Time.deltaTime * currentMovementSpeed * maxMovementSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (currentMovementSpeed > 0)
            {
                currentMovementSpeed = 0;
                currentMovementSpeedAnim -= movementSpeedChange * 3f;
            }
            currentMovementSpeed -= movementSpeedChange;
            currentMovementSpeedAnim -= movementSpeedChange;
            currentMovementSpeedAnim = Mathf.Clamp(currentMovementSpeedAnim, -1f, 1f);
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, -1f, 1f);
            this.transform.Translate(this.transform.forward * Time.deltaTime * currentMovementSpeed * maxMovementSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (currentStrafeSpeed < 0) 
            { 
                currentStrafeSpeed = 0;
                currentStrafeSpeedAnim += strafeSpeedChane * 3f;
            }
            currentStrafeSpeed += strafeSpeedChane;
            currentStrafeSpeedAnim += strafeSpeedChane;
            currentStrafeSpeedAnim = Mathf.Clamp(currentStrafeSpeedAnim, -1f, 1f);
            currentStrafeSpeed = Mathf.Clamp(currentStrafeSpeed, -1f, 1f);
            this.transform.Translate(this.transform.right * Time.deltaTime * currentStrafeSpeed * maxStrafeSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (currentStrafeSpeed > 0) 
            { 
                currentStrafeSpeed = 0;
                currentStrafeSpeedAnim -= strafeSpeedChane * 3f;
            } 
            currentStrafeSpeed -= strafeSpeedChane;
            currentStrafeSpeedAnim -= strafeSpeedChane;
            currentStrafeSpeedAnim = Mathf.Clamp(currentStrafeSpeedAnim, -1f, 1f);
            currentStrafeSpeed = Mathf.Clamp(currentStrafeSpeed, -1f, 1f);
            this.transform.Translate(this.transform.right * Time.deltaTime * currentStrafeSpeed * maxStrafeSpeed, Space.World);
        }

        animator.SetFloat("StrafeSpeed", currentStrafeSpeedAnim);
        animator.SetFloat("ForwardSpeed", currentMovementSpeedAnim);
        DiminishMovementSpeed();
        DiminishStrafeSpeed();
    }

    void DiminishMovementSpeed()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) return;

        float diminishSpeed = 0.01f;

        if (currentMovementSpeed > 0f)
        {
            currentMovementSpeed -= diminishSpeed;
            Mathf.Clamp(currentMovementSpeed, 0f, 1f);
        }

        else if (currentMovementSpeed < 0f)
        {
            currentMovementSpeed += diminishSpeed;
            Mathf.Clamp(currentMovementSpeed, -1f, 0f);
        }

        if (currentMovementSpeedAnim > 0f)
        {
            currentMovementSpeedAnim -= diminishSpeed;
            Mathf.Clamp(currentMovementSpeedAnim, 0f, 1f);
        }

        else if (currentMovementSpeedAnim < 0f)
        {
            currentMovementSpeedAnim += diminishSpeed;
            Mathf.Clamp(currentMovementSpeedAnim, -1f, 0f);
        }
    }

    void DiminishStrafeSpeed()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) return;

        float diminishSpeed = 0.01f;

        if (currentStrafeSpeedAnim > 0f)
        {
            currentStrafeSpeedAnim -= diminishSpeed;
            Mathf.Clamp(currentStrafeSpeedAnim, 0f, 1f);
        }

        else if (currentStrafeSpeedAnim < 0f)
        {
            currentStrafeSpeedAnim += diminishSpeed;
            Mathf.Clamp(currentStrafeSpeedAnim, -1f, 0f);
        }

        if (currentStrafeSpeedAnim > 0f)
        {
            currentStrafeSpeedAnim -= diminishSpeed;
            Mathf.Clamp(currentStrafeSpeedAnim, 0f, 1f);
        }

        else if (currentStrafeSpeedAnim < 0f)
        {
            currentStrafeSpeedAnim += diminishSpeed;
            Mathf.Clamp(currentStrafeSpeedAnim, -1f, 0f);
        }
    }
}
