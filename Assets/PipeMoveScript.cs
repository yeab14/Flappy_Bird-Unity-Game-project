using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 10f;
    private float currentSpeed;
    private Vector3 initialScale;
    private float fixedVerticalMovement = 0.5f; 

    void Start()
    {
        currentSpeed = baseMoveSpeed;
        initialScale = transform.localScale;
    }

    void Update()
    {
        currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);

        // Move pipes from right to left
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        // Remove the floating effect for more predictable movement
        // If you want a slight vertical movement, uncomment below:
        // transform.position += new Vector3(0, Mathf.Sin(Time.time) * fixedVerticalMovement, 0);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}

