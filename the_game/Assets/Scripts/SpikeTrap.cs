using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damage = 2;
    public float trapCycleCoolDown = 3;
    public float hitAgainCoolDown = 2;

    private bool isActive = false;
    private bool gotalreadyHit = false;
    private bool inCycle = false;
    private bool onTrap = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inCycle)
            StartCoroutine(TrapCoolDown());

        if(isActive && !gotalreadyHit && onTrap)
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.TakeDamage(damage);
            StartCoroutine(HitCoolDown());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            onTrap = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            onTrap = false;;
    }

    IEnumerator HitCoolDown()
    {
        gotalreadyHit = true;
        yield return new WaitForSeconds(hitAgainCoolDown);
        gotalreadyHit = false;
    }

    IEnumerator TrapCoolDown()
    {
        inCycle = true;
        isActive = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(trapCycleCoolDown);
        isActive = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(trapCycleCoolDown);
        inCycle = false;
    }
}
