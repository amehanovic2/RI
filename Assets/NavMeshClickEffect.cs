using UnityEngine;
using UnityEngine.AI;

public class NavMeshClickEffect : MonoBehaviour
{
    public NavMeshAgent playerAgent; // Referenca na NavMesh Agent
    public ParticleSystem clickEffect; // Particle System prefab

    void Update()
    {
        // Provjerava da li je kliknuto lijevim dugmetom miša
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Postavi destinaciju igrača na mjesto gdje je kliknuto
                if (hit.collider.CompareTag("Walkable")) // Provjerava da li je kliknuta površina hodanja
                {
                    playerAgent.SetDestination(hit.point);

                    // Instanciraj particle efekt na mjestu klika
                    if (clickEffect != null)
                    {
                        ParticleSystem effect = Instantiate(clickEffect, hit.point, Quaternion.identity);
                        effect.Play();

                        // Uništi particle efekt nakon što završi
                        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
                    }
                }
            }
        }
    }
}
