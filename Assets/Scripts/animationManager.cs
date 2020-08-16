using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;

public class animationManager : MonoBehaviour
{
    public Animator am;
    public PhotonView graphicsPV;
    public Animator graphicsAM;

    public Rig idleArmRig;
    public Rig runArmRig;

    public float animationRiggingTransitionSpeed;
    
    [HideInInspector] public bool isCrouching;
    [HideInInspector] public int speed;

	void Update()
	{
        am.SetFloat("speed", Mathf.Abs((float) speed));
        if (isCrouching)
        {
            am.SetBool("isCrouching", true);
        }/*
        if (Mathf.Abs(speed) == 2 && runArmRig.weight < 1f)
        {
            runArmRig.weight = Mathf.Clamp(runArmRig.weight + Time.deltaTime * animationRiggingTransitionSpeed, 0f, 1f);
        }
        else if (Mathf.Abs(speed) < 2 && runArmRig.weight > 0f)
        {
            runArmRig.weight = Mathf.Clamp(runArmRig.weight - Time.deltaTime * animationRiggingTransitionSpeed, 0f, 1f);
        }*/
	}

	[PunRPC]
	public void playAnimPV(string animName)
	{
		graphicsAM.CrossFade(animName, 0.25f);
	}

}
