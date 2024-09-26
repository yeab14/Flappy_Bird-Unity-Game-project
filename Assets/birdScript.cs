using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    private bool gameStarted;

    void Start()
    {
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameStarted = false; 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            if (!gameStarted)
            {
                gameStarted = true;
            }

           
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, flapStrength);
        }

       
        if (!gameStarted)
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }
}

