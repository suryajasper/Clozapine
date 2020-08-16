using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform noRecoilCam;

    public float sensitivity = 100f;
    public float recoilSpeed = 5f;
    public Transform player;
    [HideInInspector] public bool startedFire;
    [HideInInspector] public bool isShooting;
    public float lerpSpeed;

    private float xRotation = 0f;

    private Vector3 recoil;

    private System.Random random;

    void Start()
    {
        random = new System.Random();
        noRecoilCam.position = transform.position;
        noRecoilCam.rotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ApplyRecoil(float amount)
    {
        recoil += new Vector3(((float) random.NextDouble() -0.5f) * amount, (float) random.NextDouble() * amount, 0f);
    }

    public void StopRecoil()
    {
        recoil = Vector3.zero;
    }

    public void BalanceNoRecoilCam()
    {
        noRecoilCam.localRotation = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Vector3 RecoilLerp = Vector3.Lerp(transform.localRotation.eulerAngles, transform.localRotation.eulerAngles - recoil, 4f * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(xRotation + (isShooting ? RecoilLerp.y : 0f), 0f, 0f);
        noRecoilCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
    }
}
