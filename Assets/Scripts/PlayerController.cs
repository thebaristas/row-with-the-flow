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
    private float cannotMoveTimer = 0;
    private float moveUntilTimer = 0;
    private float inputCooldown = 0;

    public enum PlayerControllerMode {
        FREEFORM,
        BEAT
    }

    public PlayerControllerMode playerControllerMode = PlayerControllerMode.BEAT;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        inputCooldown = Conductor.instance.secondPerBeat / 2f;
    }

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
                        float acc = Conductor.instance.GetAccuracy();
                        forwardRatio = acc;
                        RythmUI.Instance.ShowMessage("Accurracy: " + acc);
                        cannotMoveTimer = Time.time + inputCooldown;
                        moveUntilTimer = Time.time + inputCooldown;
                    } else {
                        RythmUI.Instance.ShowMessage("Too soon");
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
