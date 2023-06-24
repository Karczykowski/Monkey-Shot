using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 35f;
    public float maxCameraMove;

    private float TargetZoom;
    private bool isZoomed=false;

    public GameObject zoom;
    public GameObject hotbar;
    public GameObject crosshairIcon;

    private void Start()
    {
        TargetZoom = Camera.main.orthographicSize;
    }
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

        if (Input.GetMouseButtonDown(1) && GameManager.instance.currentGunIndex == 2)
        {
            if (isZoomed)
            {
                TargetZoom += 10f;
                isZoomed = false;
                zoom.SetActive(false);
                hotbar.SetActive(true);
                crosshairIcon.SetActive(true);
            }
            else
            {
                TargetZoom -= 10f;
                isZoomed = true;
                zoom.SetActive(true);
                hotbar.SetActive(false);
                crosshairIcon.SetActive(false);
            }
            Camera.main.orthographicSize = TargetZoom;
        }
        if(isZoomed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, mousePosition, 0.1f);
        }
    }
}
