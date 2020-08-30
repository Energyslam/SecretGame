using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public int damage;
    AudioSource audio;
    [SerializeField]ParticleSystem muzzleFlare;
    public float recoilIntensity = 2f;
    public int recoilDuration = 10;
    public float fireRate = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void Fire()
    {
        muzzleFlare.Play();
        //audio.Play();
        audio.PlayOneShot(audio.clip);

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
}
