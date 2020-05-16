using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public struct GunInfo
    {
        public int reloadSpeed;
        public int fireRate;
        public int damage;
        public int accuracy;
        public int maxCap;

        public GunInfo(int reloadSpeed, int fireRate, int damage, int accuracy, int maxCap)
        {
            this.reloadSpeed = reloadSpeed;
            this.fireRate = fireRate;
            this.damage = damage;
            this.accuracy = accuracy;
            this.maxCap = maxCap;
        }
    }

    [Range(1f, 10f)  ] public float reloadTime;
    [Range(0.02f, 5f)] public float fireRate;
    [Range(1, 100)   ] public int damagePerHit;
    [Range(1, 100)   ] public int accuracy;
    [Range(20, 100)  ] public int range;
    [Range(1, 100)   ] public int maxCap;

    private GameManager gameManager;
    private Camera playerCam;
    private float startFire;
    private int currentCap;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerCam = gameManager.mainCamera;

        startFire = Time.time;
        currentCap = maxCap;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - startFire >= fireRate)
        {
            Shoot();
        }
        if ((Input.GetKeyDown(KeyCode.R) && currentCap < maxCap) || currentCap == 0)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        GameObject hitObject = GetRayCast(range);
        if (hitObject != null)
        {
            if (hitObject.GetComponent<Entity>() != null)
            {
                bool isHeadshot = hitObject.CompareTag("head");
                hitObject.GetComponent<Entity>().TakeDamage(damagePerHit * (isHeadshot ? 1f: gameManager.headshotMultiplier));
                GameObject bloodSplatter = Instantiate(gameManager.bloodParticle, hitObject.transform.position, gameManager.bloodParticle.transform.rotation).gameObject;
                Destroy(bloodSplatter, 5f);
            }
            else
            {
                GameObject hitEffect = Instantiate(gameManager.hitParticle, hitObject.transform.position, gameManager.hitParticle.transform.rotation).gameObject;
                Destroy(hitEffect, 5f);
            }
        }
        currentCap--;
        gameManager.mainCamera.transform.Rotate(0f, 10 - accuracy / 10f, 0f);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentCap = maxCap;
    }

    private GameObject GetRayCast(int range)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
            return hit.transform.gameObject;
        return null;
    }

}
