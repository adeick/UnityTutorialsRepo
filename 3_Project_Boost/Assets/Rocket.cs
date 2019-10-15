﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationalThrust = 100f;
    [SerializeField] float forwardThrust = 1000f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelUp;
     
    Rigidbody rigidBody;
    AudioSource audioSource;

enum State {Alive, Dead, Transcending}
State state = State.Alive;
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
        if(state == State.Alive){
            float rotationThisFrame = forwardThrust * Time.deltaTime;
            if(Input.GetKey(KeyCode.Space)){
                rigidBody.AddRelativeForce(Vector3.up * rotationThisFrame);
                if(!audioSource.isPlaying){
                    audioSource.PlayOneShot(engineSound);
                }
            }
            else{
                audioSource.Stop();
            }
        }
    }

    void Rotate(){
        if(state == State.Alive){
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
    }

    void OnCollisionEnter(Collision collision){
        if(state == State.Alive){
            switch(collision.gameObject.tag){
                case "Friendly":
                    print("OK");
                    break;
                case "Finish":
                    print("Finished");
                    state = State.Transcending;
                    audioSource.PlayOneShot(levelUp);
                    Invoke("LoadNextScene", 1f);
                    break;
                case "Danger":
                    print("Dead");
                    state = State.Dead;
                    audioSource.Stop();
                    audioSource.PlayOneShot(death);
                    Invoke("LoadNextScene", 1f);
                    break;
            }
        }
    }

    void LoadNextScene(){
        if(state == State.Transcending){
            SceneManager.LoadScene(1);
        }
        else if(state == State.Dead){
            SceneManager.LoadScene(0);
        }
        state = State.Alive;
    }
}

