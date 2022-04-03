using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riverer : MonoBehaviour
{

    public Vector2 initialVelocity;

    private Rigidbody2D body;

    [SerializeField]
    private Collider2D riverCollider;

    public Collider2D GetRiverCollider2D() { return riverCollider; }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = initialVelocity;
        body.drag = RiverSettings.Instance.linearDrag;
        body.angularDrag = RiverSettings.Instance.angularDrag;
        body.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForce(- RiverSettings.Instance.dragCoefficient * (body.velocity - RiverSettings.Instance.riverSpeed));
    }
}
