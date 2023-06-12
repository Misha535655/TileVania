using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movieSpeed = 10f;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); 
    }


    void Update()
    {
        myRigidbody.velocity = new Vector2(movieSpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other) {
        movieSpeed = -movieSpeed;
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);    
    }

}  