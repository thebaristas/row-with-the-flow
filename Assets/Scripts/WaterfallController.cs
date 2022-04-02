using UnityEngine;

public class WaterfallController : MonoBehaviour
{
    private BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.name);
        if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player collider");
            GameController.instance.GameOver();
        }
    }


}
