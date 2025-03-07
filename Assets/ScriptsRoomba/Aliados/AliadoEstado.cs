using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoEstado
{
    public AliadoIA aliadoIA;

    public void initializeVariables(AliadoIA _aliadoIA)
    {
        aliadoIA = _aliadoIA;
    }

    // Enumerador estados del aliado
    public enum ESTADO
    {
        ESPERANDO, SIGUIENDO, ATACAR
    };

    // Enumerador eventos del aliado
    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    // Estados aliado
    public ESTADO nombre;
    protected EVENTO faseActual;
    protected AliadoEstado siguienteEstado;

    // Metodos llamado al entrar
    public virtual void Entrar()
    {
        faseActual = EVENTO.ACTUALIZAR;
    }

    // Metodo que se ejecuta mientras esta activo
    public virtual void Actualizar()
    {
        faseActual = EVENTO.ACTUALIZAR;
    }

    // Metodo que se llama al salir
    public virtual void Salir()
    {
        faseActual = EVENTO.SALIR;
    }

    // Transicion entre estados dependiendo de la fase en la que este
    public AliadoEstado ProcesarEstado()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado;
        }
        return this;
    }
}