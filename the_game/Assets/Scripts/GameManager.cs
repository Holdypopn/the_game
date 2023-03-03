using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();
    public GameObject enemyToSpawn;

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
        Enemy.OnEnemyKilled += SpawnNewEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
        Enemy.OnEnemyKilled -= SpawnNewEnemy;
    }

    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
    }

    void SpawnNewEnemy(Enemy enemy)
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        Instantiate(enemyToSpawn, randomPositionOnScreen, Quaternion.identity);
    }

    void HandleEnemyDefeated(Enemy enemy)
    {
        if(enemies.Remove(enemy))
            UpdateEnemiesLeftText();
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemies.Count}";
    }
}
