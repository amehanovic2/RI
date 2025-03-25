using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    public float PCRotationSpeed = 10f;
    public Camera cam;

    private bool isDragging = false;

    private void Update()
    {
        // Provjeravamo klik mišem
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ako je kliknuto na platformu, omogućavamo rotaciju
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // Provjeravamo da li je kliknuto na platformu
                {
                    isDragging = true;
                }
            }
        }

        // Zaustavljamo rotaciju nakon puštanja klika
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            float rotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

            Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }
    }
}


/*private void Update()
{
    foreach (Touch touch in Input.touches)
    {
        Debug.Log("Touching at: " + touch.position);
        Ray camRay = cam.ScreenPointToRay(touch.position);
        RaycastHit raycastHit;

        if (Physics.Raycast(camRay, out raycastHit, 10))
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch phase Moved: " + touch.position);
                transform.Rotate(touch.deltaPosition.y * MobileRotationSpeed,
                    -touch.deltaPosition.x * MobileRotationSpeed, 0, Space.World);
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch phase Ended: " + touch.position);
            }

        }
    }
}*/

