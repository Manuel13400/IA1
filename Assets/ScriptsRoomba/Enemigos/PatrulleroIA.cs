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
        // Impide que dispare sin parar
        if (disparando)
        {
            yield break;
        }

        // Utiliza un bool para que el enemigo este en el estado disparando, que cuando se pare la corrutina en la clase AltoElFuego se vuelva false y pueda dejar de disparar
        disparando = true;

        // Mientras la corrutina este activa...
        while (true)
        {
            // Busca al objetivo, en este caso el jugador
            Vector3 objetivo = player.transform.position;
            // Instancia un proyectil
            GameObject instanciaProyectil = Instantiate(bala, transform.position, Quaternion.identity);

            // Le añade fuerza a su RigidBody en direccion al jugador
            Rigidbody rb = instanciaProyectil.GetComponent<Rigidbody>();
            rb.AddForce((objetivo - transform.position).normalized * 500f);

            // Al pasar los dos segundos destruye las balas previas para que no se queden por ahi
            yield return new WaitForSeconds(2f);

            GameObject[] balasPrevias = GameObject.FindGameObjectsWithTag("Proyectil");
            foreach (GameObject bala in balasPrevias)
            {
                Destroy(bala);
            }
        }
    }

    // Clase para que el enemigo deje de realizar las acciones de la corrutina de disparar en caso de que pierda al jugador de vista
    // Adicionalmente, cambia el estado de disparando a false para que este no siga disparando
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