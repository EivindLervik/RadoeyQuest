using UnityEngine;
using System.Collections;

public enum PlayerCutscene
{
    IceGolem
}

public class PlayerAnimator : AnimatorBase{

    private PlayerController pC;

	void Start () {
        pC = GetComponentInParent<PlayerController>();
        BindAnimator();
	}




    public void DoAction()
    {
        pC.DoAction();
    }

    public void SetControlable(int cC)
    {
        if(cC == 1)
        {
            pC.SetCanControl(true);
        }
        else if (cC == 0)
        {
            pC.SetCanControl(false);
        }
    }

    public void PressButton()
    {
        anim.SetTrigger("PressButton");
    }

    public void DoYes()
    {
        anim.SetTrigger("YES");
    }

    public void DoCutscene(PlayerCutscene c)
    {
        anim.SetTrigger(c.ToString());
    }
}
