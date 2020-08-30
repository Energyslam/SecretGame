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
    //public ParticleSystem muzzleFlash;
    public float mouseSensitivity = 1f;
    //public float fireRate = 0.03f;
    private float nextFire = 0.0f;

    private float nextRecoil = 0.0f;
    //public int damage = 10;
    Animator animator;
    AudioSource audio;
    //public Transform rifleMuzzle;

    //public float recoilIntensity = 2f;
    //public int recoilDuration = 10;
    public int currentRecoilDuration = 0;
    public bool recoiling;

    [SerializeField]Gun rifle;
    [SerializeField]Gun shotgun;

    [SerializeField]Gun currentGun;
    Quaternion tempChest = Quaternion.identity;
    public enum WeaponType
    {
        Rifle,
        Shotgun
    }

    public WeaponType weaponType = WeaponType.Rifle;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
        AssignInitialGun();
    }

    void AssignInitialGun()
    {
        if (weaponType == WeaponType.Rifle)
        {
            currentGun = rifle;
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            currentGun = shotgun;
        }
        if (!currentGun.gameObject.activeInHierarchy)
        {
            currentGun.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (SwitchWeapons())
        {

        }
        else if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Fire();
            Recoil();
        }
        RotateCamera();
    }

    void SaveCurrentSpinePosition()
    {
        tempChest = chest.rotation;
    }

    void Fire()
    {
        nextFire = Time.time + currentGun.fireRate;
        currentGun.Fire();
    }

    void Recoil()
    {
        recoiling = true;
    }


    void RotateCamera()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
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
        chest.LookAt(ray.GetPoint(distance * currentGun.recoilIntensity));
        chest.rotation *= Quaternion.Euler(offset);
    }
    void RotateSpine()
    {

        if (recoiling)
        {
            LookAtRecoil();
            currentRecoilDuration++;
            SaveCurrentSpinePosition();
            if (currentRecoilDuration > currentGun.recoilDuration)
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

    bool SwitchWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentGun != rifle)
            {
                SwitchActiveGun(rifle);

            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentGun != shotgun)
            {
                SwitchActiveGun(shotgun);
            }
        }
        else return false;
        return true;
    }

    void SwitchActiveGun(Gun gun)
    {
        currentGun.gameObject.SetActive(false);
        currentGun = gun;
        currentGun.gameObject.SetActive(true);

    }
}
