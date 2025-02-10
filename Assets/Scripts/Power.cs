using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Power : MonoBehaviour
{
    private GameObject[] enemies;

    private GameManager gameManager;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine("Fear");
        }
    }

    IEnumerator Fear()
    {
        foreach (GameObject enemy in enemies) { enemy.GetComponent<enemigo>().asustados = true; }

        yield return new WaitForSeconds(5f);

        foreach (GameObject enemy in enemies) { enemy.GetComponent<enemigo>().asustados = false; }
        Destroy(this.GameObject());
    }
}
