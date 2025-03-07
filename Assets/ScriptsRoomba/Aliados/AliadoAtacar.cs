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
        aliadoIA.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        if (!aliadoIA.tamanyoMax) { aliadoIA.transform.localScale *= 2f; }
        aliadoIA.tamanyoMax = true;
        aliadoIA.agent.speed *= 2f;
        aliadoIA.agent.SetDestination(aliadoIA.enemy.transform.position);
        aliadoIA.animator.Play("Run");

        base.Entrar();
    }

    // Metodo que se ejecuta mientras el estado Atacar esta activo
    public override void Actualizar()
    {
        siguienteEstado = new AliadoAtacar(aliadoIA);
        faseActual = EVENTO.SALIR;
    }

    // Metodo que se ejecuta al salir del estado Atacar
    public override void Salir()
    {
        base.Salir();
    }

}
