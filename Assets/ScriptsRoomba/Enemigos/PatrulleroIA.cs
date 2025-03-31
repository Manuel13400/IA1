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

    public GameObject bala;
    public bool disparando = false;

    public Coroutine coroutinaDisparo;

    void Start()
    {
        player = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();

        FSM = new Vigilar(this); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);
    }

    void Update()
    {
        FSM = FSM.ProcesarEstado();
    }

    public IEnumerator AbrirFuego()
    {
        if (disparando)
        {
            yield break;
        }

        disparando = true;

        while (true)
        {
            Vector3 objetivo = player.transform.position;
            GameObject instanciaProyectil = Instantiate(bala, transform.position, Quaternion.identity);

            Rigidbody rb = instanciaProyectil.GetComponent<Rigidbody>();
            rb.AddForce((objetivo - transform.position).normalized * 500f);


            yield return new WaitForSeconds(2f);

            GameObject[] balasPrevias = GameObject.FindGameObjectsWithTag("Proyectil");
            foreach (GameObject bala in balasPrevias)
            {
                Destroy(bala);
            }
        }
    }

    public void AltoElFuego()
    {
        if (coroutinaDisparo != null)
        {
            StopCoroutine(coroutinaDisparo);
            coroutinaDisparo = null;
        }
        disparando = false;
    }
}