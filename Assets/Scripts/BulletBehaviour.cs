using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D rigidbody;

    [SerializeField] float bulletSpeed = 15f;

    PlayerMovementScript player;

    float xSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovementScript>();
        //rotate direction of bullet when rotating player sprite
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        rigidbody.velocity = new Vector2(xSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Squid"){
        Debug.Log("hit");
        Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);    
    }

}
