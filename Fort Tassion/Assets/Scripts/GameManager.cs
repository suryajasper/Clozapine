using UnityEngine;
using System.Collections;

public class GameManager
{
    public GameObject player;
    public Camera mainCamera;
    [Range(1f, 2f)] public float headshotMultiplier;
    public ParticleSystem bloodParticle;
    public ParticleSystem hitParticle;
}
