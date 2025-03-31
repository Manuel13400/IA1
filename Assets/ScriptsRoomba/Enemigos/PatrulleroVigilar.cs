using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class Vigilar : Estado
{
    public Vigilar() : base()
    {
        Debug.Log("VIGILAR");
        nombre = ESTADO.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de andar, calcular los puntos por los que patrulla, etc...

        base.Entrar();
    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...
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
            siguienteEstado = new Atacar();
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de VIGILAR a ATACAR.
        }
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de andar, detener las corrutinas, o lo que sea...
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
                Debug.Log("Detectado!!!");
            }
            else
            {
                Debug.Log("Hay una pared en medio");
            }
        }

        return false; // DE MOMENTO NO
    }
}

