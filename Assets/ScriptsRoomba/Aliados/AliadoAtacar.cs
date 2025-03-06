using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoAtacar : AliadoEstado
{
    // Constructor que inicializa las variables
    public AliadoAtacar(AliadoIA aliado) : base()
    {
        Debug.Log("Atacando");
        nombre = ESTADO.ATACAR;
        initializeVariables(aliado);
    }

    // Metodo que se ejecuta al entrar en el estado Atacar
    public override void Entrar()
    {
        aliadoIA.GetComponent<Renderer>().material.color = Color.yellow;
        aliadoIA.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        aliadoIA.agent.speed *= 1.5f;
        aliadoIA.agent.SetDestination(aliadoIA.enemy.transform.position);
        aliadoIA.animator.Play("Run");

        base.Entrar();
    }

    // Metodo que se ejecuta mientras el estado Atacar esta activo
    public override void Actualizar()
    {
        siguienteEstado = new AliadoEsperando(aliadoIA);
        faseActual = EVENTO.SALIR;
    }

    // Metodo que se ejecuta al salir del estado Atacar
    public override void Salir()
    {
        base.Salir();
    }

    // Comprueba si el aliado puede atacar al enemigo
    public bool PuedoAtacarAlEnemigo()
    {
        if (Vector3.Distance(aliadoIA.enemy.transform.position, aliadoIA.transform.position) <= 3f)
        {
            return true;
        }
        return false;
    }
}
