using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 35f;
    public float maxCameraMove;
    void Update()
    {
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        var posx = transform.position;
        posx.x = Mathf.Clamp(posx.x, -maxCameraMove, maxCameraMove);
        transform.position = posx;
    }
}
