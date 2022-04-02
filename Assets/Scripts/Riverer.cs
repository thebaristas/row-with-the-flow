using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riverer : MonoBehaviour
{
    [SerializeField]
    Vector2 riverSpeed = new Vector2(0, 1);
    [SerializeField]
    float dragCoefficient = 1;
    [SerializeField]
    float linearDrag = 0;
    [SerializeField]
    float angularDrag = 0.5f;
    [SerializeField]
    Vector2 initialSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialSpeed;
        rb.drag = linearDrag;
        rb.angularDrag = angularDrag;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 f = - dragCoefficient * (rb.velocity + riverSpeed);
        Debug.Log(f);
        rb.AddForce(f);
    }
}
