using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NavJugador : MonoBehaviour
{
    float velocidad;
    CharacterController controller;

    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();

        controller = this.GetComponent<CharacterController>();
        velocidad = 5f;
    }

    void FixedUpdate()
    {
        //Capturo el movimiento en los ejes
        float movimientoV = Input.GetAxis("Vertical");
        float movimientoH = Input.GetAxis("Horizontal");

        Vector3 anguloTeclas = new Vector3(movimientoH, 0f, movimientoV);
        
        transform.Translate(anguloTeclas * velocidad * Time.deltaTime, Space.World);

        //Genero el vector de movimiento
        //Muevo el jugador
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
        currentPosition.y = 0f;
        transform.position = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dot"))
        {
            Destroy(other.GameObject());
            gameManagerScript.pickedDots++;
        }
    }
}