using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int minHori, maxHori;
    [SerializeField] private int minVert, maxVert;
    public GameObject[] Obstacles;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private int minDepth, maxDepth;
    public GameObject playerPos;
    private Vector3 movementPosition;
    [SerializeField] private int offSet;

    private Vector3 newSpawnPosition;
    [SerializeField] private float spawnRate;
    private float spawnTimer;
    private int rand;

    private int difCou;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacleDelay());
        InvokeRepeating("Difficulty", 2, 4);

    }

    // Update is called once per frame
    void Update()
    {

        spawnRate = Mathf.Clamp(spawnRate,0.2f,6);
        movementPosition = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z + offSet);
        SpawnAsteroid();
        transform.position = movementPosition;
    }
    void SpawnAsteroid()
    {
        Vector3 spawnPosition = new Vector3((transform.position.x + Random.Range(minHori, maxHori)), (transform.position.y + Random.Range(minVert, maxVert)), (transform.position.z + Random.Range(minDepth, maxDepth)));
        rand = Random.Range(0, 360);
        newSpawnPosition = spawnPosition;
    }

    void Difficulty()
    {
        spawnRate -= 0.2f;
    }

    void SpawnObstacle()
    {
        Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], transform.forward + newSpawnPosition, Quaternion.Euler(rand,rand,rand));
    }
    IEnumerator SpawnObstacleDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate / 20f);
            SpawnObstacle();

        }
    }

}
