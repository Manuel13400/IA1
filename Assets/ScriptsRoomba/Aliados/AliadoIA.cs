using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AliadoIA : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public NavMeshAgent agent;

    AliadoEstado FSM;

    public Animator animator;

    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        agent = GetComponent<NavMeshAgent>();

        FSM = new AliadoEsperando(this);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FSM = FSM.ProcesarEstado();
    }

    // Al chocar con el enemigo se destruyen
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
