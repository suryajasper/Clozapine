using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public Transform cameraPos;
    void FixedUpdate()
    {
        float yawCamera = cameraPos.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), 15f * Time.deltaTime);
    }
}
