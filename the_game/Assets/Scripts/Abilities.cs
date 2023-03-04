using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public GameObject circlePrefab;
    public float growFactor = 20f;
    public float maxSize;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateCircle()
    {
        GameObject circle = Instantiate(circlePrefab, transform.position, transform.rotation);
        float timer = 0;

        while(maxSize > circle.transform.localScale.x)
        {
            timer += Time.deltaTime;
            circle.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * growFactor;
            circle.transform.position = transform.position;
            yield return null;
        }

        Destroy(circle);
    }


    
}
