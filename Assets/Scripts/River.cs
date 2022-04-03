using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public Vector2 riverSpeed = new Vector2(0, -1);
    public float dragCoefficient = 1;
    public float linearDrag = 0;
    public float angularDrag = 0.5f;

    private static River _instance;

    public static River Instance { get { return _instance; } }

    private List<Rigidbody2D> floatingBodies = new List<Rigidbody2D>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void FixedUpdate() {
        foreach (Rigidbody2D body in floatingBodies) {
            body.AddForce(- River.Instance.dragCoefficient * (body.velocity - River.Instance.riverSpeed));
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        Riverer riverer = col.gameObject.GetComponent<Riverer>();
        if (riverer != null) {
            Rigidbody2D body = riverer.gameObject.GetComponent<Rigidbody2D>();
            if (!floatingBodies.Contains(body)) {
                floatingBodies.Add(body);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        Riverer riverer = col.gameObject.GetComponent<Riverer>();
        if (riverer != null) {
            Rigidbody2D body = riverer.gameObject.GetComponent<Rigidbody2D>();
            if (floatingBodies.Contains(body)) {
                floatingBodies.Remove(body);
            }
        }
    }
}
