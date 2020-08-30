using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform backside;
    public Transform frontside;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePosition();
        this.transform.LookAt(frontside);
        //Vector3 rot = this.transform.rotation.eulerAngles;
        //rot *= -1f;
        //this.transform.rotation = Quaternion.Euler(rot);
    }

    void CalculatePosition()
    {
        //this.transform.position = backside.position;
        Vector3 direction = frontside.position - backside.position;
        this.transform.position = backside.position + (direction.normalized * (direction.magnitude / 2f));
    }
}
