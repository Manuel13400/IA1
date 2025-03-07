using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoSiguiendo : AliadoEstado
{
    // Constructor que inicializa las variables
    public AliadoSiguiendo(AliadoIA aliado) : base()
    {
        nombre = ESTADO.SIGUIENDO;
        initializeVariables(aliado);
    }

    // Metodo que se ejecuta al entrar en el estado Siguiendo
    public override void Entrar()
    {
        Debug.Log("Siguiendo");

        // Color
        aliadoIA.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;

        // Tamanyo
        aliadoIA.transform.localScale *= 2;

        // Animacion
        aliadoIA.animator.Play("Run");

        base.Entrar();
    }

    // Metodo que se ejecuta mientras el estado Siguiendo esta activo
    public override void Actualizar()
    {
        if (PuedoVerAlEnemigo())
        {
            siguienteEstado = new AliadoAtacar(aliadoIA);
            faseActual = EVENTO.SALIR;
        }
        aliadoIA.agent.SetDestination(aliadoIA.player.transform.position);
    }

    // Metodo que se ejecuta al salir del estado Siguiendo
    public override void Salir()
    {
        base.Salir();
    }

    // Comprueba si el aliado puede ver al enemigo
    public bool PuedoVerAlEnemigo()
    {
        // Comprueba si quedan enemigos para que no de error
        if (aliadoIA.enemy != null)
        {
            if (Vector3.Distance(aliadoIA.enemy.transform.position, aliadoIA.transform.position) <= 4f)
            {
                return true;
            }
        }
        return false;
    }
}
