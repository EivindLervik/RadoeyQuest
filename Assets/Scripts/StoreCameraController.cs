using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoreCameraController : MonoBehaviour {

    public float transitionSpeedModefier;
    public int currentIndex;
    public NavMeshAgent player;
    public Transform[] states;

    private Transform targetTransform;

    void Start()
    {
        SetTargetTransform();
    }

	void Update () {
        // Update position
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * transitionSpeedModefier);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, Time.deltaTime * transitionSpeedModefier);

        // Change position
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0.0f){
                currentIndex++;
            }
            else
            {
                currentIndex--;
            }
            

            if (currentIndex >= states.Length)
            {
                currentIndex = 0;
            }
            else if( currentIndex < 0)
            {
                currentIndex = states.Length - 1;
            }

            SetTargetTransform();
        }
	}

    private void SetTargetTransform()
    {
        targetTransform = states[currentIndex];
        player.SetDestination(targetTransform.position);

    }
}
