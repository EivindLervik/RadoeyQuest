using UnityEngine;
using System.Collections;

public class AnimatorBase : MonoBehaviour {
    public float minSpeedThreshold;
    public float maxSpeedThreshold;

    protected Animator anim;

    protected void BindAnimator()
    {
        anim = GetComponent<Animator>();
        //print(anim);
    }

    public virtual void SetGrounded(bool g)
    {
        anim.SetBool("Grounded", g);
    }

    public virtual void SetIsMoving(bool moving)
    {
        anim.SetBool("Moving", moving);
    }

    public virtual void DoSprint(bool s)
    {
        anim.SetBool("Sprinting", s);
    }

    public virtual void DoWalk(bool w)
    {
        anim.SetBool("Walking", w);
    }

    public virtual void DoJump()
    {
        anim.SetTrigger("Jump");
    }

    public virtual void CancelJump()
    {
        anim.ResetTrigger("Jump");
    }

    public virtual void AirChange()
    {
        anim.SetTrigger("Airchange");
    }

    public virtual void CancelAirChange()
    {
        anim.ResetTrigger("Airchange");
    }

    public virtual void Landed()
    {
        anim.SetTrigger("Landed");
    }
}
