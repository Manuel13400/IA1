using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vigilar : Estado
{
    public Vigilar(PatrulleroIA enemigo) : base()
    {
        Debug.Log("VIGILAR");
        nombre = ESTADO.VIGILAR;
        inicializarVariables(enemigo);
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
        enemigoIA.GetComponent<MeshRenderer>().material.color = Color.green;

        if (enemigoIA.moviendosePuntoFinal)
        {
            enemigoIA.agent.SetDestination(enemigoIA.puntoB);
            if (Vector3.Distance(enemigoIA.puntoB, enemigoIA.transform.position) < 0.5f)
            {
                enemigoIA.moviendosePuntoFinal = false;
            }
        }
        else
        {
            enemigoIA.agent.SetDestination(enemigoIA.puntoA);
            if (Vector3.Distance(enemigoIA.puntoA, enemigoIA.transform.position) < 0.5f)
            {
                enemigoIA.moviendosePuntoFinal = true;
            }
        }

        if (PuedeVerJugador())
        {
            siguienteEstado = new Atacar(enemigoIA);
            faseActual = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
        base.Salir();
    }

    public bool PuedeVerJugador()
    {
        float distanciaConJugador = Vector3.Distance(enemigoIA.transform.position, enemigoIA.player.transform.position);
        if (distanciaConJugador <= 5f)
        {
            Vector3 direccion = (enemigoIA.player.transform.position - enemigoIA.transform.position).normalized;
            if (Physics.Raycast(enemigoIA.transform.position, direccion, out RaycastHit hit, 5f))
            {
                if (((1 << hit.collider.gameObject.layer) & enemigoIA.jugadorCapa) != 0)
                {
                    Debug.Log("¡Te encontre!");
                    return true;
                }
                else
                {
                    Debug.Log("Hay una pared en medio...");
                    return false;
                }
            }
        }
        return false;
    }
}

