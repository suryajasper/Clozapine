using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [HideInInspector] public List<Gun> guns;
    [HideInInspector] public int current;
    public PlayerController3D player;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            guns.Add(transform.GetChild(i).gameObject.GetComponent<Gun>());
        }
        guns[0].gameObject.SetActive(true);
        player.gunPositionAnimator = guns[0].gameObject.GetComponent<Animator>();
        current = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            guns[current].gameObject.SetActive(false);
            if (++current == guns.Count) current = 0;
            guns[current].gameObject.SetActive(true);
            guns[current].Activate();
            player.gunPositionAnimator = guns[current].gameObject.GetComponent<Animator>();
        }
        
    }
}
