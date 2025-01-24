using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject jugador;
    public GameObject[] dots;
    
    void Start()
    {
        jugador = GameObject.Find("Chomp");
        dots = GameObject.FindGameObjectsWithTag("Dot");
        StartCoroutine("EnemySpawner");
        StartCoroutine("ItemSpawner");
    }

    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(10f);

    }

    IEnumerator ItemSpawner()
    {


        yield return new WaitForSeconds(10f);

    }


}
