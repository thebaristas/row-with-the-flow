using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            var offset = calibrator.CalculateOffset();
            offsetText.text = "Offset: " + (1000f * offset * Conductor.instance.secondPerBeat).ToString() + " ms";
            Conductor.instance.userOffset = offset;
        }
    }

    public void Reset() {
        calibrator.Reset();
        Conductor.instance.userOffset = 0;
        offsetText.text = "Offset: 0 ms";
    }

    public void Back() {
        Conductor.instance.Stop();
        SceneManager.LoadScene(0);
    }
}
