using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Atacar : Estado
{
    public Atacar(PatrulleroIA enemigo) : base()
    {
        Debug.Log("ATACAR");
        nombre = ESTADO.ATACAR;
        inicializarVariables(enemigo);
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
        enemigoIA.GetComponent<MeshRenderer>().material.color = Color.red;

        // Si deja de poder disparar volvera al estado VIGILAR
        if (!PuedeAtacar())
        {
            siguienteEstado = new Vigilar(enemigoIA);
            faseActual = EVENTO.SALIR;
        }
    }

    public override void Salir()
    {
        base.Salir();
    }

    public bool PuedeAtacar()
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
                // Si el raycast choca con la capa del jugador...
                if (((1 << hit.collider.gameObject.layer) & enemigoIA.jugadorCapa) != 0)
                {
                    // Se queda quieto en el sitio...
                    enemigoIA.agent.speed = 0f;

                    // Y si no esta disparando, inicia la corrutina
                    if (!enemigoIA.disparando)
                    {
                        enemigoIA.coroutinaDisparo = enemigoIA.StartCoroutine(enemigoIA.AbrirFuego());
                    }

                    return true;
                }
                else
                {
                    // En caso alternativo, vuelve a moverse y deja de disparar
                    enemigoIA.AltoElFuego();
                    enemigoIA.agent.speed = 2.5f;
                    return false;
                }
            }
        } else
        {
            // Si el jugador se aleja tambien vuelve a moverse y deja de disparar
            enemigoIA.AltoElFuego();
            enemigoIA.agent.speed = 2.5f;
            return false;
        }
        enemigoIA.agent.speed = 0f;
        return true;
    }
}