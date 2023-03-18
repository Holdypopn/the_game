using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static string prefabPath = "GameManager";
    private static GameManager gm;
    
    public GameObject slime;
    public GameObject skull;

    
    public static GameManager GM
    {
        set
        {
            gm = value;
        }
        get
        {
            if(gm == null)
            {
                Object gameManagerRef = Resources.Load(prefabPath);
                GameObject gmObject = Instantiate(gameManagerRef) as GameObject;

                if(gmObject != null)
                {
                    gm = gmObject?.GetComponent<GameManager>();
                    DontDestroyOnLoad(gmObject);
                }
            }
            return gm;
        }
    }

    // Data to persist
    public float health;
    public float shield;
    public float essence;

  

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
        Instantiate(enemy, randomPositionOnScreen, Quaternion.identity);
    }

    public void SpawnEnemyGroup()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            Instantiate(slime, randomPositionOnScreen, Quaternion.identity);
        }
        
        for (int i = 0; i < 5; i++)
        {
            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            Instantiate(skull, randomPositionOnScreen, Quaternion.identity);
        }
    }
}
