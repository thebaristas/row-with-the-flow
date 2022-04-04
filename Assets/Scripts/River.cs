using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public Vector2 riverSpeed = new Vector2(0, -1);
    public float dragCoefficient = 1;
    public float linearDrag = 0;
    public float angularDrag = 0.5f;
    public AnimationCurve AltitudeDifficultyCurve;
    public float AltitudeDifficultyForce = 2f;
    public bool IsAltitudeDifficultyAppliedToAllObjects = true;

    private static River _instance;

    public static River Instance { get { return _instance; } }
    private Collider2D riverCollider;
    private float riverMinAltitude;
    private float riverMaxAltitude;

    private List<Rigidbody2D> floatingBodies = new List<Rigidbody2D>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            WaterfallController waterfallController = gameObject.GetComponentInChildren<WaterfallController>();
            PlayerPusher playerPusher = gameObject.GetComponentInChildren<PlayerPusher>();
            riverMinAltitude = waterfallController.gameObject.transform.position.y;
            riverMaxAltitude = playerPusher.gameObject.transform.position.y;
        }
    }

    void FixedUpdate() {
        foreach (Rigidbody2D body in floatingBodies) {
            body.AddForce(- River.Instance.dragCoefficient * (body.velocity - River.Instance.riverSpeed));
            if (IsAltitudeDifficultyAppliedToAllObjects || body.GetComponent<PlayerController>()) {
                float objectAltitude = body.gameObject.transform.position.y;
                float relativeAltitude = (objectAltitude - riverMinAltitude) / (riverMaxAltitude - riverMinAltitude);
                float forceRatio = AltitudeDifficultyCurve.Evaluate(relativeAltitude);
                // difficulty increase based on the distance to the bottom of the screen
                body.AddForce(forceRatio * AltitudeDifficultyForce * River.Instance.riverSpeed);
            }
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
