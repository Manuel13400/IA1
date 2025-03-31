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
        float distanciaConJugador = Vector3.Distance(enemigoIA.transform.position, enemigoIA.player.transform.position);
        if (distanciaConJugador <= 5f)
        {
            Vector3 direccion = (enemigoIA.player.transform.position - enemigoIA.transform.position).normalized;
            if (Physics.Raycast(enemigoIA.transform.position, direccion, out RaycastHit hit, 5f))
            {
                if (((1 << hit.collider.gameObject.layer) & enemigoIA.jugadorCapa) != 0)
                {
                    enemigoIA.agent.speed = 0f;

                    if (!enemigoIA.disparando)
                    {
                        enemigoIA.coroutinaDisparo = enemigoIA.StartCoroutine(enemigoIA.AbrirFuego());
                    }

                    enemigoIA.StartCoroutine("AbrirFuego");

                    return true;
                }
                else
                {
                    Debug.Log("Hay una pared en medio...");
                    enemigoIA.AltoElFuego();
                    enemigoIA.agent.speed = 2.5f;
                    return false;
                }
            }
        } else
        {
            enemigoIA.AltoElFuego();
            enemigoIA.agent.speed = 2.5f;
            return false;
        }
        enemigoIA.agent.speed = 0f;
        return true;
    }
}