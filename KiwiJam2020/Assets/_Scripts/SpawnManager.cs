using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int minHori, maxHori;
    [SerializeField] private int minVert, maxVert;
    public GameObject Asteroid1;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private int minDepth, maxDepth;
    public GameObject playerPos;
    private Vector3 movementPosition;
    [SerializeField] private int offSet;

    [SerializeField] private float spawnRate;
    private float spawnTimer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(spawnTimerCount());
        movementPosition = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z + offSet);
        SpawnAsteroid();
        transform.position = movementPosition;
    }
    void SpawnAsteroid()
    {
        Vector3 spawnPosition = new Vector3((transform.position.x + Random.Range(minHori, maxHori)), (transform.position.y + Random.Range(minVert, maxVert)), (transform.position.z + Random.Range(minVert, maxVert)));
        if (spawnTimer > spawnRate)
        {
            Instantiate(Asteroid1, transform.forward + spawnPosition, Quaternion.identity);
            spawnTimer = 0;
        }

    }
    IEnumerator spawnTimerCount()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            spawnTimer++;
        }

    }

}
