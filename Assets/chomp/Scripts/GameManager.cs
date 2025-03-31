using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Puntos")]
    public GameObject[] dots;
    public int pickedDots;
    public int totalDots;

    [Header("Interfaz")]
    public GameObject pointCanvas;
    public TextMeshProUGUI pointText;
    public GameObject enemiesCanvas;
    public TextMeshProUGUI enemiesText;

    public GameObject winScreenCanvas;
    public GameObject defeatScreenCanvas;

    public int defeatedEnemies = 0;
    public int aliveEnemies;

    GameObject temporizadorCanvas;
    TextMeshProUGUI temporizadorTexto;
    float totalSegundos = 0;

    [Header("Poderes")]
    public GameObject[] powerSpawns;
    public GameObject powerPrefab;

    [Header("Enemigos")]
    public GameObject EnemySpawn;
    public GameObject[] enemies;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;

    private BoxCollider spawnArea;

    private void Start()
    {
        startGame();
        setUI();
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalSegundos += Time.deltaTime;
        updateUI();
        if (pickedDots == totalDots) { WinScreen(); }
    }

    void startGame()
    {
        Time.timeScale = 1f;
        dots = GameObject.FindGameObjectsWithTag("Dot");

        powerSpawns = GameObject.FindGameObjectsWithTag("Power");

        EnemySpawn = GameObject.Find("EnemySpawn");
        spawnArea = EnemySpawn.GetComponent<BoxCollider>();

        StartCoroutine("EnemySpawner");
        StartCoroutine("ItemSpawner");
    }

    void setUI()
    {
        pointCanvas = GameObject.Find("puntuacion");
        pointText = pointCanvas.GetComponent<TextMeshProUGUI>();
        pickedDots = 0;
        totalDots = dots.Length;
        pointText.text = "PUNTOS\n" + pickedDots + "/" + totalDots;

        enemiesCanvas = GameObject.Find("enemiesCount");
        enemiesText = enemiesCanvas.GetComponent<TextMeshProUGUI>();

        aliveEnemies = enemies.Length;

        enemiesText.text = "ENEMIGOS\n" + aliveEnemies + " vivos\n" + defeatedEnemies + " muertos";

        winScreenCanvas = GameObject.Find("WinScreen");
        winScreenCanvas.SetActive(false);

        defeatScreenCanvas = GameObject.Find("DefeatScreen");
        defeatScreenCanvas.SetActive(false);
    }

    void updateUI()
    {
        pointText.text = "PUNTOS\n" + pickedDots + "/" + totalDots;
        aliveEnemies = enemies.Length;
        enemiesText.text = "ENEMIGOS\n" + aliveEnemies + " vivos\n" + defeatedEnemies + " muertos";
    }

    public void defeatedEnemyAdd()
    {
        defeatedEnemies++;
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (enemies.Length < 6)
            {
                Vector3 randomPosition = RandomPositionInSpwawner();
                Instantiate(enemyPrefab1, randomPosition, Quaternion.identity);

                randomPosition = RandomPositionInSpwawner();
                Instantiate(enemyPrefab2, randomPosition, Quaternion.identity);
            }
        }
    }

    Vector3 RandomPositionInSpwawner()
    {
        Bounds bounds = spawnArea.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, 0.5f, randomZ);
    }

    IEnumerator ItemSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (GameObject.FindGameObjectsWithTag("PowerPoint").Length == 0)
            {
                int randomPoint = Random.Range(0, powerSpawns.Length);
                Instantiate(powerPrefab, powerSpawns[randomPoint].transform.position, Quaternion.identity);
            }
        }
    }

    public void GameOverScreen()
    {
        defeatScreenCanvas.SetActive(true);

        temporizadorCanvas = GameObject.Find("Time");
        temporizadorTexto = temporizadorCanvas.GetComponent<TextMeshProUGUI>();

        temporizadorTexto.text = "Time: " + totalSegundos;

        Time.timeScale = 0;
    }

    void WinScreen()
    {
        winScreenCanvas.SetActive(true);

        temporizadorCanvas = GameObject.Find("Time");
        temporizadorTexto = temporizadorCanvas.GetComponent<TextMeshProUGUI>();

        temporizadorTexto.text = "Time: " + totalSegundos;

        Time.timeScale = 0;
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
