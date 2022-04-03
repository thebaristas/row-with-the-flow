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

    public enum PlayerControllerMode {
        FREEFORM,
        BEAT
    }

    public PlayerControllerMode playerControllerMode = PlayerControllerMode.BEAT;

    private float GetAccuracy() {
        float acc = Mathf.Repeat(Time.time * 120f / 60f, 1f);
        if (acc > 0.5) return acc - 1;
        return acc;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private float cannotMoveTimer = 0;
    private float moveUntilTimer = 0;
    public float inputCooldown = (60f / 120f) / 2f; // half beat (60 / BPM) / 2

    public AnimationCurve accuracyRewardCurve;

    void Update()
    {
        rotation = Input.GetAxis("Horizontal");
        body.angularVelocity = -1 * rotation * rotationSpeed;

        switch (playerControllerMode) {
            case PlayerControllerMode.FREEFORM:
                forwardRatio = Input.GetAxis("Vertical");
                break;
            case PlayerControllerMode.BEAT:
                if (Input.GetButtonDown("Jump")) {
                    if (Time.time >= cannotMoveTimer) {
                        float acc = 1f - 2f * Mathf.Abs(GetAccuracy());
                        acc = accuracyRewardCurve.Evaluate(Mathf.Clamp01(acc));
                        Debug.Log("Accuracy: " + acc);
                        forwardRatio = acc;
                        cannotMoveTimer = Time.time + inputCooldown;
                        moveUntilTimer = Time.time + inputCooldown;
                    } else {
                        Debug.Log("Too soon!");
                    }
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (playerControllerMode) {
            case PlayerControllerMode.FREEFORM:
                float now = Time.fixedTime;
                if (forwardRatio > 0 && now >= lastTimeMoved + moveInterval) {
                    // start moving
                    body.AddRelativeForce(new Vector2(0, forwardRatio * strength));
                    lastTimeMoved = now;
                }
                else if (now <= lastTimeMoved + moveDuration) {
                    // continue moving
                    body.AddRelativeForce(new Vector2(0, forwardRatio * strength));
                }
                break;
            case PlayerControllerMode.BEAT:
                if (Time.fixedTime < moveUntilTimer) {
                    body.AddRelativeForce(forwardRatio * strength * Vector2.up);
                }
                break;
        }
    }
}
