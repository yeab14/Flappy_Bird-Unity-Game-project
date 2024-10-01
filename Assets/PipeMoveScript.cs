using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 10f;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseMoveSpeed;
    }

    void Update()
    {
        currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}


