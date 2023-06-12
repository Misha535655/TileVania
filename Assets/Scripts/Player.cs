using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movieSpeed = 10f, jump = 20f, climb = 100f ;
    public Vector2 deathKick = new Vector2(10f, 10f);
    public GameObject bullet;
    public Transform gun;
    Rigidbody2D playerBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    

    bool isAlive = true;
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Jump();
        Movie();
        ClimbLadder();
        Die();
    }
     
    void Jump(){
        if(!isAlive){return;}

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

        if(Input.GetKeyDown(KeyCode.Space)){
            playerBody.AddForce(transform.up * jump);
        }
     }
    void Fier(){
        if(!isAlive){return;}
        if(Input.GetKeyDown("e")){
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }
    void Movie(){
        if(!isAlive){return;}

        float horizontalInput = Input.GetAxis ("Horizontal"); 
        transform.Translate(new Vector2(horizontalInput, 0) * movieSpeed * Time.deltaTime);
        myAnimator.SetBool("isRunning", false);

        if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)){
             transform.localScale = new Vector2(-1f, 1f);
             myAnimator.SetBool("isRunning", true);
        }
        if(Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector2(1f, 1f);
            myAnimator.SetBool("isRunning", true);
        }

     }
    void ClimbLadder(){
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myAnimator.SetBool("isClimb", false);
            playerBody.gravityScale = 3;
            return;
            }
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(0, verticalInput) * climb * Time.deltaTime);
        myAnimator.SetBool("isClimb", true);
        playerBody.gravityScale = 0;
    }

    void Die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            playerBody.velocity = deathKick;
        }
    }
}
