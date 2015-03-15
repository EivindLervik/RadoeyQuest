using UnityEngine;
using System.Collections;

public class BussSkript : MonoBehaviour {

	public float svingeFart;
	public float kjoreFart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * kjoreFart);
		transform.RotateAround(transform.position, Vector3.up, 10.0f * Time.deltaTime * svingeFart);
	}
}
