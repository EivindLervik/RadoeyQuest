using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

    public bool isMainCharacter;
    public Transform targetCamera;
    public Transform character;

    public float sprintModefier;
    public float walkModefier;
    public float acceleration;
    public float deacceleration;
    public float jumpForce;
    public Vector3 gravity;
    public LayerMask groundMask;
    public float maxGroundAngleOnJump;

    private Rigidbody body;
    private CapsuleCollider thisBox;
    private Vector3 moveDirection;
    private AnimatorBase animator;

    private bool isSprinting;
    private bool isGrounded;
    private bool isJumping;
    private bool isWalking;

    private bool previousDownwards;

	void Start () {
        body = GetComponent<Rigidbody>();
        thisBox = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<AnimatorBase>();
    }

    void Update () {
	    // None
	}

    void FixedUpdate()
    {
        /**
            Check Grounding
        **/
        RaycastHit hit;
        Vector3 origin = transform.position + (transform.up/5.0f);
        isGrounded = Physics.SphereCast(origin, thisBox.radius, Vector3.down, out hit, 0.2f, groundMask);
        //print(Vector3.Dot(hit.normal, transform.up));
        if (Vector3.Dot(hit.normal, transform.up) < (90.0f - maxGroundAngleOnJump)/90.0f || isJumping)
        {
            isGrounded = false;
        }

        /**
            Forces
        **/
        Vector3 appliedForce = new Vector3();

        if (isMainCharacter)
        {
            appliedForce += moveDirection.z * targetCamera.forward;
            appliedForce += moveDirection.x * targetCamera.right;
        }
        else
        {
            appliedForce = moveDirection;
        }

        Vector3 nyBrems = body.velocity;
        nyBrems.x /= (deacceleration * Time.fixedDeltaTime);
        nyBrems.z /= (deacceleration * Time.fixedDeltaTime);
        body.velocity = nyBrems;

        // Sprint
        if (isSprinting)
        {
            appliedForce *= sprintModefier;
        }
        if (isWalking)
        {
            appliedForce *= walkModefier;
        }
        appliedForce *= acceleration * Time.fixedDeltaTime;

        // Jump
        if (Mathf.Abs(moveDirection.y) > 0.0f && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            previousDownwards = false;
            animator.CancelAirChange();

            Vector3 hopp = body.velocity;
            hopp.y = moveDirection.y * jumpForce;
            body.velocity = hopp;

            // Jump Anim
            animator.DoJump();
        }



        // Apply Force
        body.AddForce(appliedForce + gravity);



        /**
            Facing-direction
        **/
        Vector3 nyFram = body.velocity;
        nyFram.y = 0.0f;
        if(Mathf.Abs(nyFram.magnitude) > 0.02f)
        {
            SetCharacterOrientation(nyFram);
        }



        /**
            Animation Checking
        **/
        if (isGrounded)
        {
            animator.CancelJump();
            if (previousDownwards)
            {
                previousDownwards = false;
                animator.CancelAirChange();
                animator.Landed();
                
            }
        }
        else
        {
            bool downwards = body.velocity.y < 0.0f;
            if(downwards && !previousDownwards)
            {
                animator.AirChange();
                previousDownwards = downwards;
                isJumping = false;
            }
        }
    }

    public void UpdateMoveDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void UpdateSprinting(bool sprint)
    {
        isSprinting = sprint;
    }

    public void UpdateWalking(bool walk)
    {
        isWalking = walk;
    }

    public bool isPlayerGrounded()
    {
        return isGrounded;
    }

    void OnTriggerExit(Collider collider)
    {
        string tag = collider.transform.tag;

        // Stuff sjer
    }

    public void SetCharacterOrientation(Vector3 forward)
    {
        character.forward = forward;
    }

    public void StopMovement()
    {
        body.velocity = new Vector3();
    }

    public bool IsMoving(float threshold)
    {
        print(body.velocity.magnitude + " - " + threshold);
        return body.velocity.magnitude >= threshold;
    }

    public void ToggleSmoothRigidbody()
    {
        if(body.interpolation == RigidbodyInterpolation.None)
        {
            body.interpolation = RigidbodyInterpolation.Interpolate;
        }
        else
        {
            body.interpolation = RigidbodyInterpolation.None;
        }
    }
}
