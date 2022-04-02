using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strength = 30f;
    public float rotationSpeed = 100f;
    public float swimInterval = 0.5f;
    public float swimDuration = 0.3f;
    private Rigidbody2D body;
    private float forwardRatio = 0f;
    private float rotation = 0f;
    private float swimCooldown = 0f;

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
        body.angularVelocity = -1 * rotation * rotationSpeed;
        if (swimCooldown <= 0) {
            body.AddRelativeForce(new Vector2(0, forwardRatio * strength));
        }
        if (swimCooldown < - swimDuration) {
            ResetSwimCooldown();
        }
        swimCooldown -= Time.fixedDeltaTime;
    }

    void ResetSwimCooldown() {
        swimCooldown = swimInterval;
    }
}
