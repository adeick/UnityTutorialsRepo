using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    private void Shoot()
    {
        ProcessRaycast();
        PlayMuzzleFlash();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            print("You missed.");
            return;
        }
    }
    private void PlayMuzzleFlash(){
        MuzzleFlash.Play();
    }
}
