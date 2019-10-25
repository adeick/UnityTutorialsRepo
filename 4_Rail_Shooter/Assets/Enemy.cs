using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Explosion Particle Effect")][SerializeField] GameObject explosionFX; 
    // Start is called before the first frame update
    [SerializeField] int killPoints = 12;
    ScoreBoard scoreBoard;
    void Start()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other){
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        scoreBoard.ScoreHit(killPoints);
    }
}
