using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Explosion Particle Effect")][SerializeField] GameObject explosionFX; 
    [SerializeField] float levelLoadDelay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
       print("Handler Working"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        SendMessage("PlayerDeath");
        explosionFX.SetActive(true);
        Invoke("LevelRestart", levelLoadDelay);
    }

    void LevelRestart(){
        SceneManager.LoadScene(1);
    }
}
