using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationalThrust = 100f;
    [SerializeField] float forwardThrust = 1000f;
    
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    void Thrust(){
        float rotationThisFrame = forwardThrust * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up * rotationThisFrame);
            if(!audioSource.isPlaying){
                audioSource.Play();
            }
        }
        else{
            audioSource.Stop();
        }
    }

    void Rotate(){
        float rotationThisFrame = rotationalThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; // stop Physics so we can handle rocket
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //Resume Physics
    }

    void OnCollisionEnter(Collision collision){
        switch(collision.gameObject.tag){
            case "Friendly":
                print("OK");
                break;
            case "Danger":
                print("Dead");
                break;
        }
    }

}

