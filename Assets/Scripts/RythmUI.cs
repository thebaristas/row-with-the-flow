using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmUI : MonoBehaviour
{
    public Image main;
    public Text accuracyDisplay;
    public AnimationCurve curve;

    private Vector2 initialSize;

    private static RythmUI _instance;

    public static RythmUI Instance { get { return _instance; } }

    public void ShowMessage(string message) {
        accuracyDisplay.text = message;
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
    void Start()
    {
        initialSize = main.rectTransform.rect.size;
    }

    // Update is called once per frame
    void Update()
    {
        float d = 1f + 0.4f * curve.Evaluate(Conductor.instance.GetDistanceToNextBeat());
        main.rectTransform.localScale = new Vector2(d, d);
    }
}
