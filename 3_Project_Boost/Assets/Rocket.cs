using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    bool collisions = true;
    [SerializeField] float rotationalThrust = 100f;
    [SerializeField] float forwardThrust = 1000f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelUp;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;
    
     
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
        if(Debug.isDebugBuild){
            DebugKeys();
        }
    }
    void DebugKeys(){
        if (Input.GetKeyDown(KeyCode.L)){
            state = State.Transcending;
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisions = !collisions;
        }
    }
    void Thrust(){
        if(state == State.Alive){
            float rotationThisFrame = forwardThrust * Time.deltaTime;
            if(Input.GetKey(KeyCode.Space)){
                rigidBody.AddRelativeForce(Vector3.up * rotationThisFrame);
                thrustParticles.Play();
                if(!audioSource.isPlaying){
                    audioSource.PlayOneShot(engineSound);
                }
            }
            else{
                audioSource.Stop();
                thrustParticles.Stop();
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
                    successParticles.Play();
                    Invoke("LoadNextScene", levelLoadDelay);
                    break;
                case "Danger":
                    if(collisions){
                        print("Dead");
                        state = State.Dead;
                        audioSource.Stop();
                        audioSource.PlayOneShot(death);
                        deathParticles.Play();
                        Invoke("LoadNextScene", levelLoadDelay);
                    }
                    break;
            }
        }
    }

    void LoadNextScene(){
        if(state == State.Transcending){
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 4);
        }
        else if(state == State.Dead){
            SceneManager.LoadScene(0);
        }
        deathParticles.Stop();
        successParticles.Stop();
        state = State.Alive;
    }
}

