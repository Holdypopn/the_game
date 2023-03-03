using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float damage = 1;
    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void OnBecameInvisible() 
    {
         Destroy(gameObject);
    }
   
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
            anim.SetTrigger("onDeath");
            rb.velocity = Vector3.zero;
            Destroy(gameObject, 0.5f);
        }
    }
}
