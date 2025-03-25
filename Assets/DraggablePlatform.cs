using UnityEngine;

public class DraggablePlatform : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    public bool restrictToXAxis = true; // Restrict movement to one axis (X-axis)

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        // Calculate offset between platform position and mouse position
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;

            if (restrictToXAxis)
            {
                // Restrict movement to the X-axis (adjust based on your platform's design)
                newPosition = new Vector3(newPosition.x, transform.position.y, transform.position.z);
            }
            else
            {
                // Optionally, restrict movement to other axes if needed
                newPosition = new Vector3(transform.position.x, transform.position.y, newPosition.z);
            }

            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
