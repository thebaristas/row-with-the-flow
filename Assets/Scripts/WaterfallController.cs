using UnityEngine;

public class WaterfallController : MonoBehaviour
{
    public AnimationCurve shrinkerRotationController;
    private BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    void ShrinkObject(GameObject gameObject) {
        Shrinker shrinker = gameObject.AddComponent<Shrinker>();
        Vector3 endPosition = gameObject.transform.position - new Vector3(0, 3 * bc.bounds.extents.y, 0);
        shrinker.Run(endPosition, () =>
        {
            DeleteObject(gameObject);
        }, shrinkerRotationController);
        if (gameObject.GetComponent<PlayerController>() != null)
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    void DeleteObject(GameObject gameObject) {
        Destroy(gameObject);
        if (gameObject.GetComponent<PlayerController>()) {
            GameController.instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        ShrinkObject(col.gameObject);
    }
}
