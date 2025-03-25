using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    public GameObject platformMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformMove.GetComponent<Animator>().Play("Platform");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
