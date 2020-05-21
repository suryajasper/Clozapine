using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public float sensitivity;

    public Transform player;
    [SerializeField] private Vector3 offset;

    private float xRot = 0.0f;

    void Start()
    {
        transform.position = player.transform.position + offset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OffsetForwardPosition(float zoffset)
    {
        transform.position += transform.forward * zoffset;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
