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

        // El enemigo empieza en puntoA y va hasta puntoB. Mientras este moviendose a puntoB, el bool moviendosePuntoFinal sera true
        if (enemigoIA.moviendosePuntoFinal)
        {
            // Elige como destino puntoB
            enemigoIA.agent.SetDestination(enemigoIA.puntoB);
            // Una vez llega, cambia el bool moviendosePuntoFinal a false
            if (Vector3.Distance(enemigoIA.puntoB, enemigoIA.transform.position) < 0.5f)
            {
                enemigoIA.moviendosePuntoFinal = false;
            }
        }
        // Una vez haya cambiado el bool moviendosePuntoFinal a false, el enemigo volvera a puntoA
        else
        {
            // Elige como destino puntoA
            enemigoIA.agent.SetDestination(enemigoIA.puntoA);
            // Una vez llega, cambia el bool moviendosePuntoFinal a true
            if (Vector3.Distance(enemigoIA.puntoA, enemigoIA.transform.position) < 0.5f)
            {
                enemigoIA.moviendosePuntoFinal = true;
            }
        }

        // Si la condicion de la clase PuedoVerJugador da true, el enemigo pasa al estado ATACAR
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
        // Calcula la distancia entre el enemigo y el jugador
        float distanciaConJugador = Vector3.Distance(enemigoIA.transform.position, enemigoIA.player.transform.position);
        // A partir de cierta distancia, calcula si esta en su rango
        if (distanciaConJugador <= 5f)
        {
            // Calcula la direccion en la que invocar el raycast
            Vector3 direccion = (enemigoIA.player.transform.position - enemigoIA.transform.position).normalized;
            // Si hay algo en la trayectoria del raycast procede
            if (Physics.Raycast(enemigoIA.transform.position, direccion, out RaycastHit hit, 5f))
            {
                // Si el raycast choca con la capa del jugador devuelve true, y en caso contrario false
                if (((1 << hit.collider.gameObject.layer) & enemigoIA.jugadorCapa) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}

