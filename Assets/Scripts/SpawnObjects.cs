using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] enemy;
    private float randX;
    private Vector2 whereToSpawn;
    public float spawnRate; //2f
    private float nextSpawn = 0.0f;
    private int randEnemy;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (Time.time > nextSpawn)
        {
            int randEnemy = Random.Range(0, enemy.Length); //random enemy spawn based on length of sprites
            nextSpawn = Time.time + spawnRate; //time until next spawn
            randX = Random.Range(7f, -9f); //the range where enemy spawns
            whereToSpawn = new Vector2(randX, transform.position.y); //the position where it spawns
            Instantiate(enemy[randEnemy], whereToSpawn, Quaternion.identity);
        }
    }
}
