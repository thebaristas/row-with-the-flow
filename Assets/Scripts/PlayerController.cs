using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strength = 30f;
    public float rotationSpeed = 100f;
    public float moveInterval = 0.5f;
    public float moveDuration = 0.3f;
    private float lastTimeMoved = 0f;
    private Rigidbody2D body;
    private float forwardRatio = 0f;
    private float rotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardRatio = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        float now = Time.fixedTime;
        body.angularVelocity = -1 * rotation * rotationSpeed;
        if (forwardRatio > 0 && now >= lastTimeMoved + moveInterval) {
            // start moving
            body.AddRelativeForce(new Vector2(0, forwardRatio * strength));
            lastTimeMoved = now;
        }
        else if (now <= lastTimeMoved + moveDuration) {
            // continue moving
            body.AddRelativeForce(new Vector2(0, forwardRatio * strength));
        }
    }
}
