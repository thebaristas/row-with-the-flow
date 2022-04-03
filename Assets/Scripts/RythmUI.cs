using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmUI : MonoBehaviour
{
    public float BPM = 120f;
    public Image main;
    public AnimationCurve curve;

    private Vector2 initialSize;

    private float GetDistanceToNextBeat() {
        return Mathf.Repeat(Time.time * BPM / 60f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        initialSize = main.rectTransform.rect.size;
    }

    // Update is called once per frame
    void Update()
    {
        float d = 1f + 0.4f * curve.Evaluate(GetDistanceToNextBeat());
        main.rectTransform.localScale = new Vector2(d, d);
    }
}
