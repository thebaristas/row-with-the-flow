using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSettings : MonoBehaviour
{
    public Vector2 riverSpeed = new Vector2(0, 1);
    public float dragCoefficient = 1;
    public float linearDrag = 0;
    public float angularDrag = 0.5f;

    private static RiverSettings _instance;

    public static RiverSettings Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}
