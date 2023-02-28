using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI enemiesLeftText;
    List<Enemy> enemies = new List<Enemy>();

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyDefeated;
    }

    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        UpdateEnemiesLeftText();
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
