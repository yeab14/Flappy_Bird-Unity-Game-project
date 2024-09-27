using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5f;  
    public float gravityScale = 1f;  
    private bool gameStarted;

    void Start()
    {
        myRigidbody.gravityScale = gravityScale;
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
            Flap();  
        }
        if (!gameStarted)
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }
    void Flap()
    {
        myRigidbody.velocity = new Vector2(0, flapStrength);
    }
}
