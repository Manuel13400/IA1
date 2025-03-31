using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class PatrulleroIA : MonoBehaviour
{
    public GameObject player;

    public NavMeshAgent agent;

    Estado FSM;

    public Vector3 puntoA;
    public Vector3 puntoB;

    public bool moviendosePuntoFinal = true;

    public LayerMask jugadorCapa;
    public LayerMask paredCapa;

    void Start()
    {
        player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();

        FSM = new Vigilar(); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);
    }

    void Update()
    {
        FSM = FSM.ProcesarEstado();
    }
}