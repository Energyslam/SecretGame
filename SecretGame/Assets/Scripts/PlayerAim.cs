using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Animator animator;
    AudioSource audio;
    public Transform rifleMuzzle;
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
        muzzleFlash.Play();
        animator.SetTrigger("Shoot");
        audio.Play();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Demon"))
            {
                hit.collider.gameObject.GetComponent<DemonMovement>().HitReaction(10, hit.point);
            }
            Debug.Log("hit");
        }

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
        
        if (cameraRotation.x <350f && cameraRotation.x > 300f)
        {
            cameraRotation.x = 350f;
        }
        else if (cameraRotation.x > 20f && cameraRotation.x < 100f)
        {
            cameraRotation.x = 20f;
        }
        else if (cameraRotation.x > 20f && cameraRotation.x < 350f)
        {
            cameraRotation.x = 0f;
        }

        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
        this.transform.rotation = Quaternion.Euler(playerRotation);

    }

    void LateUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }
        Debug.Log("aaa" + Cursor.lockState);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        chest.LookAt(ray.GetPoint(distance));
        chest.rotation *= Quaternion.Euler(offset);
        Debug.DrawRay(rifleMuzzle.position, rifleMuzzle.forward * 3f, Color.red);
    }
}
