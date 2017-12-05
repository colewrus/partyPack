using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour {

    // Use this for initialization
    public float impact = 4f;
    public ParticleSystem deathEffect;

 
    public static int EnemiesAlive = 0;

    private void Awake()
    {        
        EnemiesAlive++;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.relativeVelocity.magnitude > impact)
        {
            Die();
        }
    }


    void Die()
    {
        EnemiesAlive--;        
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        
        if (EnemiesAlive <= 0)
        {
            Camera.main.GetComponent<CameraFollow>().levelEnd = true;                   
        }

        Destroy(gameObject);
    }


}
