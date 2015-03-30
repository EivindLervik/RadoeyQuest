using UnityEngine;
using System.Collections;

public class RaceCarBehaviour : MonoBehaviour {

	public float groundDistance = 1.0f;
	public float akselerasjon = 100.0f;
	public float maxSpeed = 2.0f;
	public float gravity = -10.0f;
	public float svingeFart = 2.0f;
	public Animator animator;

	private float currentSpeed;
	private Rigidbody body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		if(animator == null){
			Debug.LogWarning("Ingen animator funnet!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//sjekkDistanse();
		bevegelse ();
	}

	private void bevegelse (){
		Vector3 kraft = Vector3.up * gravity;

		if(Input.GetAxis("Vertical") > 0){
			if(body.velocity.magnitude < maxSpeed){
				kraft += (transform.forward * Time.deltaTime * akselerasjon);
			}
		}
		if(Input.GetAxis("Vertical") < 0){
			if(body.velocity.magnitude < maxSpeed){
				kraft += (-transform.forward * Time.deltaTime * akselerasjon);
			}
		}
		if(Input.GetAxis("Strobe") > 0){
			if(body.velocity.magnitude < maxSpeed){
				kraft += (transform.right * Time.deltaTime * akselerasjon);
			}
		}
		if(Input.GetAxis("Strobe") < 0){
			if(body.velocity.magnitude < maxSpeed){
				kraft += (-transform.right * Time.deltaTime * akselerasjon);
			}
		}
		if(Input.GetAxis("Horizontal") > 0){
			transform.Rotate(transform.up, svingeFart * Time.deltaTime * 100);
		}
		if(Input.GetAxis("Horizontal") < 0){
			transform.Rotate(transform.up, svingeFart * Time.deltaTime * -100);
		}


		body.AddForce(kraft);
	}

	private void sjekkDistanse(){
		RaycastHit[] hit;
		Ray ray = new Ray(transform.position, Vector3.down);
		hit = Physics.RaycastAll(ray, 5);
		bool funnet = false;
		int iterator = 0;
		while(!funnet && iterator<hit.Length){
			if(hit[iterator].transform.tag == "Bane"){
				funnet = true;
			}else{
				iterator++;
			}
		}

		// Lag og sett ny posisjon
		if(funnet){
			Vector3 groundToShip = (hit[iterator].normal.normalized * maxSpeed);
			Vector3 newTransform = hit[iterator].point + groundToShip;
			transform.position = newTransform;
		}
	}
}
