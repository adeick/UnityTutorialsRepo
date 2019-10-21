using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 3f);//3 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame(){
      SceneManager.LoadScene(1);
      print("Load Scene 1");
    }
}
