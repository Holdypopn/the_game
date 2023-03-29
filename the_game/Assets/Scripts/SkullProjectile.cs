using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectile : MonoBehaviour
{
    Vector3 targetPosition;
    public float speed;
    public float damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.FindWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }

        if(col.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
