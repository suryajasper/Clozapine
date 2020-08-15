using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class animationManager : MonoBehaviour
{
    public Animator am;
    public PhotonView graphicsPV;
    public Animator graphicsAM;

    [HideInInspector] public bool isCrouching;
    [HideInInspector] public int speed;

	void Update()
	{
        am.SetFloat("speed", Mathf.Abs((float) speed));
        if (isCrouching)
        {
            am.SetBool("isCrouching", true);
        }
	}

	public void stopAnim()
	{
	}

	[PunRPC]
	public void playAnimPV(string animName)
	{
		graphicsAM.CrossFade(animName, 0.25f);
	}

}
