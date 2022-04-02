using UnityEngine;

public delegate void ShrinkDoneCallback();

public class Shrinker : MonoBehaviour
{
    public ShrinkDoneCallback callback;
    // Time it takes in seconds to shrink from starting scale to target scale.
    public float ShrinkDuration = 1f;

    private float targetSize = 0.1f;
    // The starting scale
    Vector3 startScale;
    Vector3 startPosition;
    Vector3 targetPosition;

    // T is our interpolant for our linear interpolation.
    float t = 0;
    private bool isRunning = false;

    public void Run(Vector3 endPosition, ShrinkDoneCallback callback, float targetSize = 0.1f) {
        // initialize stuff in OnEnable
        startScale = transform.localScale;
        startPosition = transform.localPosition;
        t = 0;
        this.targetPosition = endPosition;
        this.callback = callback;
        this.targetSize = targetSize;
        isRunning = true;
    }

    void Update() {
        if (isRunning) {
            // Divide deltaTime by the duration to stretch out the time it takes for t to go from 0 to 1.
            t += Time.deltaTime / ShrinkDuration;

            // Lerp wants the third parameter to go from 0 to 1 over time. 't' will do that for us.
            Vector3 newScale = Vector3.Lerp(startScale, Vector3.one * targetSize, t);
            transform.localScale = newScale;

            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.localPosition = newPosition;

            transform.Rotate(Vector3.forward, 2);

            // We're done! We can disable this component to save resources.
            if (t > 1) {
                callback();
                enabled = false;
            }
        }
    }
}
