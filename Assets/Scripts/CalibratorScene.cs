using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibratorScene : MonoBehaviour
{
    public Text offsetText;
    Calibrator calibrator;

    // Start is called before the first frame update
    void Start()
    {
        Conductor.instance.Play();
        calibrator = new Calibrator();
        calibrator.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            calibrator.Observe(Conductor.instance.GetDistanceToClosestBeat());
            offsetText.text = "Offset: " + calibrator.CalculateOffset().ToString();
            Debug.Log(calibrator.CalculateOffset());
        }
    }

    public void Reset() {
        calibrator.Reset();
        offsetText.text = "-";
    }
}
