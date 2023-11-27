using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //variable to adjust run speed
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletspawn;

    [SerializeField] Vector2 deathState = new Vector2 (5f,5f);
 
    //user input is stored in moveInput
    Vector2 moveInput;

    Rigidbody2D rigidbody;

    Animator animator;

    CapsuleCollider2D capsulerCollider;
    BoxCollider2D feetCollider;
    AudioPlayer audioplayer;

    float defaultGravity;

    bool isAlive = true;

    void Awake() {
        audioplayer= FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsulerCollider = GetComponent<CapsuleCollider2D>();
        defaultGravity = rigidbody.gravityScale;
        feetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!isAlive){return;}
        Run();
        FlipPlayer();
        Climb();
        Die();
    }

    // inputvalue is stored in moveInput as vector2
    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }


    void Run()
    {

        //stop player from being able to move up and down (only want this when on a ladder)
        Vector2 playerVelocity = new Vector2(moveInput.x *runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;

        //changing to running animation when running, using the horizontal speed to set the bool as true
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning",playerHasHorizontalSpeed);
    }

    void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        if(value.isPressed)
        {
            
            rigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Climb()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            //fix bug where gravity remains 0 after leaving ladder.
            rigidbody.gravityScale = defaultGravity;
            return;

        }

        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, moveInput.y *climbSpeed);
        rigidbody.velocity = climbVelocity;

        //prevent sliding on ladder
        rigidbody.gravityScale = 0f;
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
        Instantiate(bullet, bulletspawn.position, transform.rotation);
        audioplayer.PlayShootingClip();
    }

    void Die(){
        if(capsulerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Death");
            rigidbody.velocity = deathState;
            //updates life count
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
