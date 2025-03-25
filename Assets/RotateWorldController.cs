using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Camera cam;

    public static bool isDraggingWorld = false; // Indikator da li se rotira svijet
    private float dragThreshold = 0.1f; // Vrijeme nakon kojeg drag počinje
    private float mouseDownTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time; // Bilježi vrijeme kada je pritisnut klik
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time - mouseDownTime > dragThreshold) // Ako klik traje dovoljno dugo, aktivira se drag
            {
                isDraggingWorld = true;

                // Računanje rotacije na osnovu pokreta miša
                float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
                float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

                // Rotacija oko globalne Y i X osi
                transform.Rotate(Vector3.up, -rotX, Space.World); // Rotacija oko Y osi
                transform.Rotate(cam.transform.right, rotY, Space.World); // Rotacija oko X osi
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDraggingWorld = false; // Završava drag
        }
    }
}
