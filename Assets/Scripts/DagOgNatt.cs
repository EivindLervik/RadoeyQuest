using UnityEngine;
using System.Collections;

public class DagOgNatt : MonoBehaviour {

    public int minutes;
    public int hours;
	public float speedModefier;

    private Light light;
    private float realTime;

	void Start () {
        light = GetComponentInChildren<Light>();
	}

	void FixedUpdate () {
        // Update sun rotation
        Vector3 rot = transform.eulerAngles;
        rot.x = ((minutes + (hours * 60)) / 1440.0f) * 360.0f;
        transform.Rotate(Vector3.right, (Time.fixedDeltaTime * speedModefier) / Mathf.PI);

        // Calculate time
        realTime += Time.fixedDeltaTime * speedModefier;
        minutes = (int) ((transform.rotation.x / 360.0f) * 60.0f);
        if(minutes >= 60)
        {
            hours++;

            if(hours >= 24)
            {
                hours = 0;
            }
        }
        if (realTime >= 60.0f)
        {
            realTime -= 60.0f;
        }
	}
}
