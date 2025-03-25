using UnityEngine;

public class PlayerAttachToPlatform : MonoBehaviour
{
    private Transform originalParent;

    void Start()
    {
        originalParent = transform.parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(other.transform); // Igrač postaje dijete platforme (kocke)
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(originalParent); // Igrač se vraća na originalnog roditelja
        }
    }
}
