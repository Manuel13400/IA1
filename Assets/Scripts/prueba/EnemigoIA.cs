using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{
    public GameObject jugador;
    public Renderer color;
    public Estado FSM;

    void Start()
    {
        jugador = GameObject.Find("Player");
        color = this.GetComponent<Renderer>();

        FSM = new Vigilar(); // CREAMOS EL ESTADO INICIAL DEL NPC
        FSM.inicializarVariables(this);
    }

    void Update()
    {
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }
}