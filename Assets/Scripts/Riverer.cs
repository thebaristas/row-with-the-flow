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
        body.drag = River.Instance.linearDrag;
        body.angularDrag = River.Instance.angularDrag;
        body.gravityScale = 0;
    }
}
