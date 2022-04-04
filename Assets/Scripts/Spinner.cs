using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float rps = 0.5f; // rotations per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward, rps * 360 * Time.deltaTime);
    }
}
