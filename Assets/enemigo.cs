using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{
    GameObject jugador;
    NavMeshAgent agente;
    
    void Start()
    {
        jugador = GameObject.Find("Chomp");
        agente = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jugador != null)
        {
            agente.SetDestination(jugador.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(jugador);
        }
    }
}
