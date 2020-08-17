using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public static float maxReload = 7f;
    [SerializeField] private LayerMask hitLayers;

    [Header("properties")]
    public float adsRecoil;
    public float hipRecoil;
    public float fireRate;
    [Range(10, 100)] public int damage;
    [Range(10, 100)] public int range;
    [Range(10, 100)] public int mobility;
    public float adsSpeed;
    public int magCap;
    public int maxAmmo;

    public bool singleShots;
    public bool isShotgun;

    [Header("animations")]
    public AnimationClip [] reload;

    [Header("Info")]
    public string gunName;

    [Header("Positions")]
    public Transform muzzleLoc;

    [Header("Prefabs")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem bloodEffect;
    public ParticleSystem metalHitEffect;

    private int currentMagCap;
    private int currentTotalCap;
    private bool activated;
    private bool isReloading;
    private float reloadLength;

    private float shootTimer = -1f;
    private float reloadTimer = -1f;

    [HideInInspector] public Animator animator;
    private CameraLook camera;

    void Start()
    {
        currentTotalCap = maxAmmo;
        currentMagCap = magCap;
        animator = GetComponent<Animator>();
        Activate();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraLook>();

        reloadLength = 0f;
        foreach (AnimationClip reloadClip in reload)
            reloadLength += reloadClip.length;
    }

    public void DeActivate()
    {
        activated = false;
    }

    public static Quaternion Vector3ToQuaternion(Vector3 vec3)
    {
        return Quaternion.Euler(vec3);
    }

    public void Activate()
    {
        activated = true;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        GameObject ammoUI = GameObject.FindGameObjectWithTag("ammo");
        ammoUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentMagCap.ToString();
        ammoUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentTotalCap.ToString();
        ammoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gunName;
    }

    private IEnumerator MakeInactiveAfterSeconds(float secs, GameObject gameObj)
    {
        yield return new WaitForSeconds(secs);

        //Game object will turn off
        gameObj.SetActive(false);
    }

    private void Shoot()
    {
        camera.startedFire = false;
        if (shootTimer == -1 || (currentMagCap > 0 && Time.time-shootTimer >= fireRate && currentMagCap > 0))
        {
            camera.ApplyRecoil(adsRecoil);
            if (singleShots)
            {
                animator.SetInteger("Fire", isShotgun ? 0: 1);
                //animator.SetInteger("Fire", -1);
            }
            shootTimer = Time.time;
            currentMagCap--;
            UpdateAmmoUI();
            RaycastHit hit;
            Vector3 rayRot = camera.transform.TransformDirection(Vector3.forward);
            if (!Input.GetMouseButton(1))
            {
                System.Random random = new System.Random();
                rayRot += new Vector3((((float) random.NextDouble()) - 0.5f) * 2f * hipRecoil, (((float)random.NextDouble()) - 0.5f) * 2f * hipRecoil, 0);
                Debug.Log(new Vector3((((float)random.NextDouble()) - 0.5f) * 2f * hipRecoil, (((float)random.NextDouble()) - 0.5f) * 2f * hipRecoil, 0));
            }
            if (Physics.Raycast(transform.position, rayRot, out hit, range, hitLayers))
            {
                PlayerController3D enemy = hit.collider.gameObject.GetComponent<PlayerController3D>();
                if (enemy != null)
                {
                    if (enemy.head.Equals(hit.collider))
                        enemy.applyDamage(damage * 1.25f);
                    else
                        enemy.applyDamage(damage);
                    Destroy(Instantiate(bloodEffect, hit.point, Quaternion.identity), 1f);
                }
                else
                {
                    Destroy(Instantiate(metalHitEffect, hit.point, Quaternion.identity), 4f);
                }
            }
        }
        if (currentMagCap == 0)
        {
            isReloading = true;
            GameObject.FindGameObjectWithTag("ReloadFill").GetComponent<TimerFiller>().StartTimer(reloadLength);
        }
    }

    private void StopShooting()
    {
        shootTimer = -1f;
    }

    void Update()
    {
        //adsArchive();
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Sight", true);
            animator.SetInteger("Reload", -1);
            isReloading = false;
            reloadTimer = -1f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Sight", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!singleShots)
                animator.SetInteger("Fire", 2);
            animator.SetInteger("Reload", -1);
            isReloading = false;
            reloadTimer = -1f;
            camera.BalanceNoRecoilCam();
            camera.isShooting = true;
        }
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            camera.isShooting = false;
            animator.SetInteger("Fire", -1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            GameObject.FindGameObjectWithTag("ReloadFill").GetComponent<TimerFiller>().StartTimer(reloadLength);
        }
        if (isReloading && currentTotalCap != 0 && currentMagCap != magCap)
        {
            camera.startedFire = false;
            animator.SetInteger("Reload", 1);
            
            if (reloadTimer == -1f)
            {
                reloadTimer = Time.time;
            }
            else if (Time.time - reloadTimer >= reloadLength)
            {
                if (magCap - currentMagCap <= currentTotalCap)
                {
                    currentTotalCap -= magCap - currentMagCap;
                    currentMagCap = magCap;
                }
                else
                {
                    currentMagCap += currentTotalCap;
                    currentTotalCap = 0;
                }
                reloadTimer = -1f;
                UpdateAmmoUI();
                animator.SetInteger("Reload", -1);
            }
        }
    }
}
