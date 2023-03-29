using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackCoolDown = 2;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRangeToPlayer(GameObject player)
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    public bool CanSeePlayer(Transform viewPoint, GameObject player)
    {
        bool canSeePlayer = false;

        RaycastHit2D hit = Physics2D.Linecast(viewPoint.position, player.transform.position, 1 << LayerMask.NameToLayer("PlayerLayer"));

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
                canSeePlayer = true;
            else
                canSeePlayer = false;

            Debug.DrawLine(viewPoint.position, hit.point, Color.red);
        }
        else
            Debug.DrawLine(viewPoint.position, hit.point, Color.yellow);

        return canSeePlayer;
    }
}
