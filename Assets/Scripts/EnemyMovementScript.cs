using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //move enemy along x axis
        rigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        flipEnemy();
    }
    
   void flipEnemy(){
        transform.localScale = new Vector2 (Mathf.Sign(rigidbody.velocity.x), 1f);
    }
}
