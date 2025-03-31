using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{
    GameObject jugador;
    NavMeshAgent agente;

    float agenteSpeed;

    public bool asustados = false;
    GameObject hidePoint;

    GameManager gameManager;
    
    void Start()
    {
        findObjects();
        setAgent();
    }

    void findObjects()
    {
        jugador = GameObject.Find("Chomp");
        hidePoint = GameObject.Find("HidePoint");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void setAgent()
    {
        agente = this.GetComponent<NavMeshAgent>();
        agenteSpeed = agente.speed;
    }

    void Update()
    {
        if (!asustados) {
            agente.SetDestination(jugador.transform.position);
            agente.speed = agenteSpeed; 
        }

        else if (asustados) {
            agente.SetDestination(hidePoint.transform.position);
            agente.speed = agenteSpeed * 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if          (other.CompareTag("Player") && asustados == false)  { gameManager.GameOverScreen(); } 
        else if     (other.CompareTag("Player") && asustados == true)   { Destroy(this.GameObject()); gameManager.defeatedEnemyAdd(); }
    }
}
