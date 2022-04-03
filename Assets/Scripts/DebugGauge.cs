using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugGauge : MonoBehaviour
{
    public string title {
        get { return _title; }
        set {
            _title = value;
            UpdateUI();
        }
    }

    public float value {
        get { return _value; }
        set {
            _value = value;
            UpdateUI();
        }
    }

    public Text text;
    public Image gaugeImage;

    [SerializeField]
    private string _title;
    [SerializeField]
    private float _value;

    public void UpdateUI() {
        text.text = title + ": " + value;
        gaugeImage.rectTransform.localScale = new Vector3(value, 1, 1);
    }
}
