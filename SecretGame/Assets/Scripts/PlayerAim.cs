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
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash.Play();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        chest.LookAt(ray.GetPoint(distance));
        chest.rotation *= Quaternion.Euler(offset);
    }
}
