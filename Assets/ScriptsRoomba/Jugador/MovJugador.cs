using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovJugador : MonoBehaviour
{
    float velocidad;
    CharacterController controller ;
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        velocidad = 3f;
    }

    void Update()
    {

        //Capturo el movimiento en los ejes
        float movimientoV = Input.GetAxis("Vertical");
        float movimientoH = Input.GetAxis("Horizontal");

        Vector3 anguloTeclas = new Vector3(movimientoH, 0f, movimientoV);

        transform.Translate(anguloTeclas * velocidad * Time.deltaTime, Space.World);

        //Genero el vector de movimiento
        //Muevo el jugador
        //transform.position += anguloTeclas * velocidad * Time.deltaTime;
        controller.Move(anguloTeclas * velocidad * Time.deltaTime);

        if (anguloTeclas != null && anguloTeclas != Vector3.zero)
        {
            transform.forward = anguloTeclas * 1;
            transform.rotation = Quaternion.LookRotation(anguloTeclas);
        }

        PlayerToTheFloor();

    }

    void PlayerToTheFloor()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0.5f;
        transform.position = currentPosition;
    }

}