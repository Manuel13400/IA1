using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuperPower : MonoBehaviour
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
            foreach (GameObject enemy in enemies) { Destroy(enemy.GameObject()); gameManager.defeatedEnemyAdd(); }
            Destroy(this.gameObject);
        }
    }
}
