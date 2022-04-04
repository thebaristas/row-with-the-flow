using UnityEngine;

public class PlayerPusher : MonoBehaviour
{
    public float pushStrength = 10f;
    private Rigidbody2D playerBody;

    void FixedUpdate() {
        if (playerBody != null) {
            playerBody.AddForce(pushStrength * River.Instance.riverSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        PlayerController controller = col.GetComponent<PlayerController>();
        if (controller != null) {
            playerBody = controller.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        PlayerController controller = col.GetComponent<PlayerController>();
        if (controller != null) {
            playerBody = null;
        }
    }
}
