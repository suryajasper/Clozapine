using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    private bool stateIn = true;

    public Animator moveOut;

    void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            moveOut.Play("moveOut", 0, stateIn ? -1f: 1f);
            stateIn = !stateIn;
        }
    }
}
