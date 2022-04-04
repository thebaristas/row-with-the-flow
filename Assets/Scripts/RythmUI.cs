using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmUI : MonoBehaviour
{
    public Image main;
    public Text accuracyDisplay;
    public AnimationCurve curve;
    public float fadeSpeed = 1000;

    public DebugGauge debugNextDistanceGauge;
    public DebugGauge debugClosestDistanceGauge;
    public DebugGauge debugAccuracyGauge;


    private RectTransform rectTransform;
    private Vector2 initialSize;

    private static RythmUI _instance;

    public static RythmUI Instance { get { return _instance; } }

    public void ShowMessage(string message) {
        ShowMessage(message, Color.white);
    }

    public void ShowMessage(string message, Color colour) {
        accuracyDisplay.text = message;
        accuracyDisplay.transform.position = main.transform.position;
        accuracyDisplay.color = colour;
        StopAllCoroutines();
        StartCoroutine(Fade());
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        initialSize = main.rectTransform.rect.size;
        rectTransform = GetComponent<RectTransform>();
        accuracyDisplay.color = Color.clear;
    }

    Vector2 GetTopCentrePosition() {
        Vector3[] v = new Vector3[4];
        rectTransform.GetWorldCorners(v);
        return new Vector2(v[2][0]/2, v[2][1]);
    }

    IEnumerator Fade() {
        // Apologies in advance for this very hacked-together code
        yield return new WaitForSeconds(.25f);
        Color c = accuracyDisplay.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            c.a = alpha;
            accuracyDisplay.color = c;
            yield return new WaitForSeconds(.01f);
        }
        c.a = 0;
        accuracyDisplay.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug
        debugNextDistanceGauge.value =  Conductor.instance.GetDistanceToNextBeat();
        debugClosestDistanceGauge.value = Conductor.instance.GetDistanceToClosestBeat();
        debugAccuracyGauge.value = Conductor.instance.GetAccuracy();
        // End debug

        float d = 1f + 0.4f * curve.Evaluate(Conductor.instance.GetDistanceToNextBeat());
        main.rectTransform.localScale = new Vector2(d, d);

        accuracyDisplay.transform.position = Vector2.MoveTowards(accuracyDisplay.transform.position, GetTopCentrePosition(), 300 * Time.deltaTime);
    }
}
