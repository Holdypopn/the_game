using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalHandling : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            //play portal animation
            //scene transition
            SceneManager.LoadScene(levelName);
        }
    }
}
