using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAim : MonoBehaviour
{
    public Transform chest;
    Vector3 worldPosition = Vector3.zero;
    public float distance = 5f;
    public Vector3 offset;
    public ParticleSystem muzzleFlash;
    public float mouseSensitivity = 1f;
    public float fireRate = 0.03f;
    private float nextFire = 0.0f;

    public float recoilRate = 0.03f;
    private float nextRecoil = 0.0f;
    public int damage = 10;
    Animator animator;
    AudioSource audio;
    public Transform rifleMuzzle;

    public float recoilIntensity = 20f;
    public int recoilDuration = 3;
    public int currentRecoilDuration = 0;
    public bool recoiling;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
    }

    private void Update()
    {   
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Fire();
        }
        HandleCursorLock();
        RotateCamera();
    }

    void Fire()
    {
        nextFire = Time.time + fireRate;
        Recoil();
        muzzleFlash.Play();
        animator.SetTrigger("Shoot");
        audio.Play();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //TODO: Implement a better system for hit registration
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Demon"))
            {
                hit.collider.gameObject.GetComponent<DemonMovement>().HitReaction(damage, hit.point);
            }
        }
    }

    void Recoil()
    {
        recoiling = true;
    }

    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        Vector3 playerRotation = this.transform.rotation.eulerAngles;
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;

        playerRotation.y += rotAmountX;
        cameraRotation.x -= rotAmountY;
        
        //if (cameraRotation.x <350f && cameraRotation.x > 300f)
        //{
        //    cameraRotation.x = 350f;
        //}
        //else if (cameraRotation.x > 20f && cameraRotation.x < 100f)
        //{
        //    cameraRotation.x = 20f;
        //}
        //else if (cameraRotation.x > 20f && cameraRotation.x < 350f)
        //{
        //    cameraRotation.x = 0f;
        //}

        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
        this.transform.rotation = Quaternion.Euler(playerRotation);

    }

    void LateUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked) //&& WorldState._instance.GetCurrentState != WorldState.State.UNDERWORLD)
        {
            return;
        }
        else
        {
            RotateSpine();
        }
    }


    void LookAtMouse()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        chest.LookAt(ray.GetPoint(distance));
        chest.rotation *= Quaternion.Euler(offset);
    }

    void LookAtRecoil()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        chest.LookAt(ray.GetPoint(distance * recoilIntensity));
        chest.rotation *= Quaternion.Euler(offset);
    }
    void RotateSpine()
    {
        if (recoiling)
        {
            LookAtRecoil();
            currentRecoilDuration++;
            if (currentRecoilDuration > recoilDuration)
            {
                currentRecoilDuration = 0;
                recoiling = false;
            }
        }
        if (!recoiling)
        {
            LookAtMouse();
        }
    }
}
