using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject targetObject;
    public Vector3 cameraOffset;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetObject.transform.position +cameraOffset; 
    }
}
