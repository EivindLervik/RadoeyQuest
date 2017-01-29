using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    public bool canControl;

    private CharacterMove characterMove;
    private PlayerAnimator animator;

    private Collider currentTrigger;
    private NavMeshAgent agent;

    void Start () {
        characterMove = GetComponent<CharacterMove>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<PlayerAnimator>();
	}

	void Update () {

        bool sprinting = false;
        bool walking = false;
        Vector3 move = new Vector3();

        if (canControl)
        {
            // Variables
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            move.Normalize();
            move.y = Input.GetAxis("Jump");
            Vector3 planeMove = move;
            planeMove.y = 0.0f;

            sprinting = Input.GetAxis("Sprint") > 0.0f;
            if (characterMove.isPlayerGrounded())
            {
                walking = Input.GetAxis("Walk") > 0.0f && !sprinting;
            }

            // Animate
            bool moving = (planeMove != Vector3.zero || agent.velocity.magnitude > 0.0f);
            animator.SetIsMoving(moving);
            animator.DoSprint(sprinting);
            animator.DoWalk(walking);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                animator.DoYes();
            }
            if(agent.velocity.magnitude > 0.0f)
            {

            }

            // Interact
            if (Input.GetButtonDown("Interact") && currentTrigger != null && !characterMove.IsMoving(0.01f))
            {
                // To interaction
            }
        }

        // Do movement
        characterMove.UpdateMoveDirection(move);
        characterMove.UpdateSprinting(sprinting);
        characterMove.UpdateWalking(walking);
    }

    public void DoAction()
    {
        // Do action
    }

    public void SetCanControl(bool cC)
    {
        //characterMove.StopMovement();
        canControl = cC;
    }

    void OnTriggerEnter(Collider collider)
    {
        // On trigger
    }

    void OnTriggerExit(Collider collider)
    {
        string tag = collider.transform.tag;

        switch (tag)
        {
            case "EndTrigger":
                SetCanControl(false);
                break;
            default:
                break;
        }

        currentTrigger = null;
    }
}
