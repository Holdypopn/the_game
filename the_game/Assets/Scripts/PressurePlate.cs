using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameManager gameManager;
    public Enemy enemy;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = Color.red;
            gameManager.SpawnNewEnemy(enemy);            
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(PressurePlateReset());
        }

        if(col.gameObject.CompareTag("Projectile"))
        {
            spriteRenderer.color = Color.red;
            gameManager.SpawnEnemyGroup();            
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(PressurePlateReset());
        }
    }

    IEnumerator PressurePlateReset()
    {
        yield return new WaitForSeconds(3);
        spriteRenderer.color = Color.white;
        GetComponent<Collider2D>().enabled = true;
    }
}
