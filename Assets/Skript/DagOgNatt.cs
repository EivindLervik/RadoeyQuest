using UnityEngine;
using System.Collections;

public class DagOgNatt : MonoBehaviour {

	public float dagFart;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround(transform.position, transform.up, 1.0f * Time.deltaTime * dagFart);
	}
}
