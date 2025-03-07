using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoEsperando : AliadoEstado
{
    // Constructor que inicializa las variables
    public AliadoEsperando(AliadoIA aliado) : base()
    {
        nombre = ESTADO.ESPERANDO;
        initializeVariables(aliado);
    }

    // Metodo que se ejecuta al entrar en el estado Esperando
    public override void Entrar()
    {
        Debug.Log("Vigilando");

        // Color
        aliadoIA.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.magenta;

        // Animacion
        aliadoIA.animator.Play("Idle");

        base.Entrar();
    }

    // Metodo que se ejecuta mientras el estado Esperando esta activo
    public override void Actualizar()
    {
        if (PuedoVerAlJugador())
        {
            siguienteEstado = new AliadoSiguiendo(aliadoIA);
            faseActual = EVENTO.SALIR;
        }
    }

    // Metodo que se ejecuta al salir del estado Esperando
    public override void Salir()
    {
        base.Salir();
    }

    // Comprueba si el aliado puede ver al jugador
    public bool PuedoVerAlJugador()
    {
        if (Vector3.Distance(aliadoIA.player.transform.position, aliadoIA.transform.position) <= 3f)
        {
            return true;
        }
        return false;
    }
}
