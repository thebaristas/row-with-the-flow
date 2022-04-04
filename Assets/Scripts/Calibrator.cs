using System.Collections;
using System.Collections.Generic;

public class Calibrator
{
    private List<float> points;

    public Calibrator() {
        points = new List<float>();
    }

    public void Observe(float value) {
        points.Add(value);
    }

    public void Reset() {
        points.Clear();
    }

    public float CalculateOffset() {
        if (points.Count == 0) return 0;

        float sum = 0;
        foreach (var p in points) {
            sum += p;
        }
        return sum / points.Count;
    }
}
