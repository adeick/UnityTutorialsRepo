using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int healthPoints = 100;

    public void TakeDamage(int damage){
        healthPoints -= damage;
        if(healthPoints < 0){
            Destroy(gameObject);
        }
    }
}
