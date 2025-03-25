using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Animator animator;
    public ParticleSystem clickEffect;

    void Update()
    {
        // Kretanje agenta samo kada nije aktivna rotacija svijeta
        if (!RotateWorldController.isDraggingWorld && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Postavi destinaciju igrača na mjesto gdje je kliknuto
                if (hit.collider.CompareTag("Walkable")) // Provjerava da li je kliknuta površina hodanja
                {
                    agent.SetDestination(hit.point);

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

        // Ažuriranje brzine za animator
        float speed = agent.velocity.magnitude;

        // Postavljanje animacije na osnovu brzine
        if (speed > 0.1f)
        {
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsMoving", false);
        }
    }
}
