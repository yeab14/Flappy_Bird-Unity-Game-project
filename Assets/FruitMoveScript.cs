using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMoveScript : MonoBehaviour
{
    public float baseMoveSpeed = 5f;     // Speed at which the fruit moves
    public float speedIncreaseRate = 0.1f; // How fast the speed increases over time
    public float maxSpeed = 10f;         // Maximum speed the fruit can reach
    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseMoveSpeed;  // Start with the base speed
    }

    void Update()
    {
        // Increase the speed of the fruit gradually up to the max speed
        currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);

        // Move the fruit to the left
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        // Destroy the fruit when it moves past the left edge of the screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}

