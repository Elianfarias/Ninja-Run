using UnityEngine;

[ExecuteAlways]
public class CameraFilled : MonoBehaviour
{
    [SerializeField]
    float targetWorldWidth = 16f; // tu ancho de dise�o
    
    void LateUpdate()
    {
        var cam = GetComponent<Camera>();
        cam.orthographicSize = targetWorldWidth / cam.aspect / 2f;
    }
}
