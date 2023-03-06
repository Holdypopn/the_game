using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject enemyToSpawn;

    private void OnEnable()
    {
        //Enemy.OnEnemyKilled += SpawnNewEnemy;
    }

    private void OnDisable()
    {
        //Enemy.OnEnemyKilled -= SpawnNewEnemy;
    }

    public void SpawnNewEnemy(Enemy enemy)
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Instantiate(enemyToSpawn, randomPositionOnScreen, Quaternion.identity);
    }
}
