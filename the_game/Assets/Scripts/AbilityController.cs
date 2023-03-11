using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public GameObject iceCirclePrefab;
    public GameObject fireCircleShotPrefab;
    public float growFactor = 20f;
    public float maxSize;

    
    public int numberOfObjects = 10;
    public float radius = 1f;
    public float projectileSpeed = 10f;
    private bool QisInCoolDown = false;
    private bool EisInCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !QisInCoolDown)
        {
            StartCoroutine(ActivateIceCircle());
        }

        if(Input.GetKeyDown(KeyCode.E) && !EisInCoolDown)
        {
            StartCoroutine(FireCircleShot());
        }
    }

    public IEnumerator ActivateIceCircle()
    {
        QisInCoolDown = true;
        GameObject circle = Instantiate(iceCirclePrefab, transform.position, transform.rotation);
        float timer = 0;

        while(maxSize > circle.transform.localScale.x)
        {
            timer += Time.deltaTime;
            circle.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * growFactor;
            circle.transform.position = transform.position;
            yield return null;
        }

        Destroy(circle);

        yield return new WaitForSeconds(5f);
        QisInCoolDown = false;
    }

    public IEnumerator FireCircleShot()
    {
        EisInCoolDown = true;
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, y, 0);
            float angleDegrees = angle*Mathf.Rad2Deg;
            Quaternion rot = transform.rotation * Quaternion.Euler(0, 0, angleDegrees);
            GameObject gameObject = Instantiate(fireCircleShotPrefab, pos, rot);
            Vector2 dir = gameObject.transform.position - transform.position;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed, ForceMode2D.Impulse);

        }
        
        yield return new WaitForSeconds(5f);
        EisInCoolDown = false;
    }
}
